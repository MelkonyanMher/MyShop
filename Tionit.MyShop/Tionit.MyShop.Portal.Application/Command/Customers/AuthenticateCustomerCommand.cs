using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Tionit.AuditLogging;
using Tionit.Enterprise;
using Tionit.Enterprise.Validation;
using Tionit.ShopOnline.Application.AuthOptions;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Application.Utilities;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Command.Customers.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Command.Customers
{
    public class AuthenticateCustomerCommand
    {
        #region Fields

        private readonly IRepository<Customer> customerRepository;
        private readonly IUserInfoProvider userInfoProvider;
        private readonly IAuditLogger auditLogger;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IDateTimeProvider dateTimeProvider;

        #endregion Fields

        #region Constructor

        public AuthenticateCustomerCommand(IRepository<Customer> customerRepository, IUserInfoProvider userInfoProvider, IAuditLogger auditLogger,
                                            IAccessRightChecker accessRightChecker, IDateTimeProvider dateTimeProvider)
        {
            this.customerRepository = customerRepository;
            this.userInfoProvider = userInfoProvider;
            this.auditLogger = auditLogger;
            this.accessRightChecker = accessRightChecker;
            this.dateTimeProvider = dateTimeProvider;
        }

        #endregion Constructors

        #region Methods

        public async Task<AuthenticateCustomerResultModel> Execute(AuthenticateCustomerInputModel input)
        {
            //права доступа
            accessRightChecker.CheckIsAnonymous();

            //предоброботка
            input.CheckAndPrepare();

            if(userInfoProvider.UserType == UserType.System)
                throw new InvalidOperationException("Запрошена аутентификация для системной задачи");

            //валидация
            Validate.IsNotNullOrWhitespace(input.UserName).WithErrorMessage("Логин должен быть указан");
            Validate.IsNotNullOrWhitespace(input.Password).WithErrorMessage("Пароль должен быть указан");

            //расчитываем хеш пароля
            var passwordHash = HashHelper.GetHashString(input.Password);

            Customer customer = customerRepository.FirstOrDefault(c => c.UserName == input.UserName && c.PasswordHash == passwordHash);
            Validate.IsNotNull(customer).WithErrorMessage("Указаны неверные логин/пароль");

            await auditLogger
                .Info("Произведен вход в систему")
                .RelatedObject(customer)
                .User(customer.UserName)
                .LogAsync();

            //Выставляем клайм
            var claims = new System.Collections.Generic.List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, input.UserName), //логин
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRole.Admin.ToString()), //роль
                new Claim(ClaimTypes.NameIdentifier, customer.AuthTokenId.ToString())
            };

            // генерируем токен
            var jwt = new JwtSecurityToken(
                issuer: CustomerAuthOptions.Issuer,
                audience: AdminAuthOptions.Audience,
                notBefore: dateTimeProvider.UtcNow,
                claims: claims,
                signingCredentials: new SigningCredentials(CustomerAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            AuthenticateCustomerResultModel result = new AuthenticateCustomerResultModel
            {
                AccessToken = encodedJwt,
                Name = customer.Name,
                UserName = customer.UserName,
                Role = UserRole.Customer
            };

            return result;
        }

        #endregion Methods
    }
}
