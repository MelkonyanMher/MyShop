using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

namespace Tionit.MyShop.Backoffice.Application.Commands.Administrators
{
    public class ChangeAdministratorPasswordCommand
    {
        #region Fields

        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAppUserInfoProvider userInfoProvider;
        private readonly IRepository<Administrator> administraorRepository;
        private readonly IChangesSaver changesSaver;
        private readonly IAuditLogger auditLogger;
        private readonly IDateTimeProvider dateTimeProvider;
        
        #endregion Fields

        #region Constructor

        public ChangeAdministratorPasswordCommand(IAccessRightChecker accessRightChecker, IAppUserInfoProvider userInfoProvider, 
                                                  IRepository<Administrator> administraorRepository, IChangesSaver changesSaver, 
                                                  IAuditLogger auditLogger, IDateTimeProvider dateTimeProvider)
        {
            this.accessRightChecker = accessRightChecker;
            this.userInfoProvider = userInfoProvider;
            this.administraorRepository = administraorRepository;
            this.changesSaver = changesSaver;
            this.auditLogger = auditLogger;
            this.dateTimeProvider = dateTimeProvider;
        }

        #endregion Constructor

        #region Methods

        public async Task<AuthenticateAdministratorResultModel> Execute(ChangeAdministratorPasswordInputModel input)
        {
            //права
            accessRightChecker.CheckIsAdmin();

            //предобработка
            input.CheckAndPrepare();

            //валидации
            Validate.IsNotNull(input.OldPassword).WithErrorMessage("Старый пароль должен быть указан");
            Validate.IsNotNull(input.NewPassword).WithErrorMessage("Новый пароль должен быть указан");
            Validate.IsTrue(input.NewPassword == input.NewPasswordConfirmation)
                    .WithErrorMessage("Новый пароль и подтверждение нового пароля должны совподать");
            //При необходимости убрать коментарий
            //Validate.IsTrue(input.NewPassword.Length > 5).WithErrorMessage("Длина пароля должна быть не меньше 6 символов");
            
            Administrator administrator = await administraorRepository.FirstOrDefaultAsync(a => a.UserName == userInfoProvider.UserName);
            Validate.IsNotNull(administrator).WithErrorMessage("Администратор не найден");

            administrator.PasswordHash = HashHelper.GetHashString(input.NewPassword);
            administrator.AuthTokenId = Guid.NewGuid();

            //сохраняем
            await changesSaver.SaveChangesAsync();

            //логируем
            await auditLogger
                .Info($"Внутранний пользователь {administrator.UserName} обновил свой пароль")
                .RelatedObject(administrator)
                .LogAsync();
            
            // выставляем кляймы
            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, administrator.UserName), // логин
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
