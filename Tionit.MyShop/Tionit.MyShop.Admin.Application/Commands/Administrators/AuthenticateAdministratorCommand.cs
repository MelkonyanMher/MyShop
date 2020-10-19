using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tionit.AuditLogging;
using Tionit.Enterprise;
using Tionit.Enterprise.Validation;
using Tionit.MyShop.Application.AuthOptions;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Application.Utilities;
using Tionit.MyShop.Backoffice.Application.Commands.Administrators.Models;
using Tionit.MyShop.Domain;
using Tionit.Persistence;

namespace Tionit.MyShop.Backoffice.Application.Commands
{
    /// <summary>
    /// Аутентификация администратора
    /// </summary>
    public class AuthenticateAdministratorCommand
    {
        #region Fields

        private readonly IRepository<Administrator> administratorRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAuditLogger auditLogger;
        private readonly IDateTimeProvider dateTimeProvider;
        #endregion Fields

        #region Constructor

        public AuthenticateAdministratorCommand(IRepository<Administrator> administratorRepository, IAccessRightChecker accessRightChecker,
                                                IAuditLogger auditLogger, IDateTimeProvider dateTimeProvider)
        {
            this.administratorRepository = administratorRepository;
            this.accessRightChecker = accessRightChecker;
            this.auditLogger = auditLogger;
            this.dateTimeProvider = dateTimeProvider;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Выполняет аутентификацию администратора
        /// </summary>
        public async Task<AuthenticateAdministratorResultModel> Execute(AuthenticateAdministratorInputModel input)
        {
            //Права доступа
            accessRightChecker.CheckIsAnonymous();

            //Предобработка
            input.CheckAndPrepare();

            //Валидации
            Validate.IsNotNull(input.UserName).WithErrorMessage("Логин должен быть указан");
            Validate.IsNotNull(input.Password).WithErrorMessage("Пароль должен быть указан");

            var passwordHash = HashHelper.GetHashString(input.Password);
            Administrator administrator =
                await administratorRepository.FirstOrDefaultAsync(a => a.UserName == input.UserName.ToLower() && a.PasswordHash == passwordHash);
            Validate.IsNotNull(administrator).WithErrorMessage("Указаны неверные логин/пароль");

            //Логирование
            await auditLogger
                .Info("Произведен вход в систему")
                .RelatedObject(administrator)
                .User(administrator.UserName)
                .LogAsync();

            // выставляем кляймы
            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, input.UserName), // логин
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRole.Admin.ToString()), // роль
                new Claim(ClaimTypes.NameIdentifier, administrator.AuthTokenId.ToString())
            };
            // генерируем токен
            var jwt = new JwtSecurityToken(
                issuer: AdminAuthOptions.Issuer,
                audience: AdminAuthOptions.Audience,
                notBefore: dateTimeProvider.UtcNow,
                claims: claims,
                signingCredentials: new SigningCredentials(AdminAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            AuthenticateAdministratorResultModel result = new AuthenticateAdministratorResultModel
            {
                AccessToken = encodedJwt,
                Email = administrator.Email,
                UserName = administrator.UserName,
                Role = UserRole.Admin
            };

            return result;
        }
        
        #endregion Methods
    }
}
