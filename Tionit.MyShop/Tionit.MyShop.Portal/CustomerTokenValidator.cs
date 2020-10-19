using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Tionit.ShopOnline.Application.AuthOptions;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Portal
{
    public class CustomerTokenValidator
    {
        public const string TokenCookiesKey = "OSH_Customer_Token";

        /// <summary>
        /// Валидирует токен
        /// </summary>
        public static bool Validate(HttpContext httpContext)
        {
            string token = httpContext.Request.Cookies[TokenCookiesKey];

            if (string.IsNullOrWhiteSpace(token) || token == "null")
                return false;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена
                ValidIssuer = CustomerAuthOptions.Issuer, // строка, представляющая издателя

                ValidateAudience = true, // будет ли валидироваться потребитель токена
                ValidAudience = CustomerAuthOptions.Audience, // установка потребителя токена

                ValidateLifetime = false, // будет ли валидироваться время существования

                IssuerSigningKey = CustomerAuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                ValidateIssuerSigningKey = true // валидация ключа безопасности
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var user = handler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            // доступ открыт только админам
            return user.IsInRole(UserRole.Customer.ToString());
        }
    }
}
