using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tionit.Enterprise;
using Tionit.ShopOnline.Application.AuthOptions;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.InteropServices;

namespace Tionit.ShopOnline.Portal
{
    public class UserSession
    {
        #region Consts

        private const string NameKey = "osh.portal.name";
        private const string TokenKey = "osh.portal.token";
        private const string SessionStartedKey = "osh.portal.issessionstarted";

        #endregion Consts

        #region Fields

        private readonly ILocalStorageService localStorage;
        private readonly IAppUserInfoSetter appUserInfoSetter;
        private readonly Cookies cookies;

        #endregion Fields

        #region Constructor

        public UserSession(ILocalStorageService localStorage, IAppUserInfoSetter appUserInfoSetter, Cookies cookies)
        {
            this.localStorage = localStorage;
            this.appUserInfoSetter = appUserInfoSetter;
            this.cookies = cookies;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Сессия начата?
        /// </summary>
        public bool IsSessionStarted { get; private set; }

        /// <summary>
        /// Данные о сессии загружены
        /// </summary>
        public bool IsLoaded { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Загружает сессию пользователя
        /// </summary>
        /// <returns></returns>
        public async Task LoadSession()
        {
            Name = await localStorage.GetItemAsync<string>(NameKey);
            Token = await localStorage.GetItemAsync<string>(TokenKey);
            IsSessionStarted = await localStorage.GetItemAsync<bool>(SessionStartedKey);

            await cookies.SetCookies(CustomerTokenValidator.TokenCookiesKey, Token);

            SetUserInfo();

            IsLoaded = true;
        }

        /// <summary>
        /// Начинает сессию пользователя
        /// </summary>
        /// <param name="name">имя пользователя</param>
        /// <param name="token">токен пользователя</param>
        public async Task StartSession(string name, string token)
        {
            await localStorage.SetItemAsync(NameKey, name);
            await localStorage.SetItemAsync(TokenKey, token);
            await localStorage.SetItemAsync(SessionStartedKey, true);

            Name = name;
            Token = token;
            IsSessionStarted = true;

            await cookies.SetCookies(CustomerTokenValidator.TokenCookiesKey, Token);

            SetUserInfo();
        }

        /// <summary>
        /// Завершает сессию пользователя
        /// </summary>
        public async Task FinishSession()
        {
            await localStorage.RemoveItemAsync(NameKey);
            await localStorage.RemoveItemAsync(TokenKey);
            await localStorage.SetItemAsync(SessionStartedKey, false);

            Name = null;
            Token = null;
            IsSessionStarted = false;

            await cookies.RemoveCookies(CustomerTokenValidator.TokenCookiesKey);

            SetUserInfo();
        }

        private void SetUserInfo()
        {
            if (Token != null)
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidIssuer = CustomerAuthOptions.Issuer, // строка, представляющая издателя

                    ValidateAudience = false, // будет ли валидироваться потребитель токена
                    ValidAudience = CustomerAuthOptions.Audience, // установка потребителя токена

                    ValidateLifetime = false, // будет ли валидироваться время существования

                    IssuerSigningKey = CustomerAuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                    ValidateIssuerSigningKey = true // валидация ключа безопасности
                };

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(Token, tokenValidationParameters, out SecurityToken validatedToken);

                appUserInfoSetter.UserName = user.Identity.Name;
                appUserInfoSetter.UserType = UserType.Authenticated;
                appUserInfoSetter.UserRole = UserRole.Customer;
                string userTokenIdStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(userTokenIdStr))
                    appUserInfoSetter.UserTokenId = Guid.Parse(userTokenIdStr);
            }
            else
            {
                appUserInfoSetter.UserName = null;
                appUserInfoSetter.UserType = UserType.Anonymous;
                appUserInfoSetter.UserTokenId = Guid.Empty;
            }
        }

        #endregion Methods
    }
}
