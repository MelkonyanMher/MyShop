using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using Tionit.Enterprise;
using Tionit.MyShop.Application.AuthOptions;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Admin.InteropServices;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Admin
{
    public class UserSession
    {
        #region Consts

        private const string NameKey = "osh.backoffice.name";
        private const string TokenKey = "osh.backoffice.token";
        private const string SessionStartedKey = "osh.backoffice.issessionstarted";

        #endregion Consts

        #region Constructor

        public UserSession(ILocalStorageService localStorage, IAppUserInfoSetter appUserInfoSetter, Cookies cookies)
        {
            this.localStorage = localStorage;
            this.appUserInfoSetter = appUserInfoSetter;
            this.cookies = cookies;
        }

        #endregion Constructor

        #region Fields

        private readonly ILocalStorageService localStorage;
        private readonly IAppUserInfoSetter appUserInfoSetter;
        private readonly Cookies cookies;

        #endregion Fields

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

            await cookies.SetCookies(AdminTokenValidator.TokenCookiesKey, Token);

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

            await cookies.SetCookies(AdminTokenValidator.TokenCookiesKey, Token);

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

            await cookies.RemoveCookies(AdminTokenValidator.TokenCookiesKey);

            SetUserInfo();
        }

        private void SetUserInfo()
        {
            if (Token != null)
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidIssuer = AdminAuthOptions.Issuer, // строка, представляющая издателя

                    ValidateAudience = false, // будет ли валидироваться потребитель токена
                    ValidAudience = AdminAuthOptions.Audience, // установка потребителя токена

                    ValidateLifetime = false, // будет ли валидироваться время существования

                    IssuerSigningKey = AdminAuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                    ValidateIssuerSigningKey = true // валидация ключа безопасности
                };

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(Token, tokenValidationParameters, out SecurityToken validatedToken);

                appUserInfoSetter.UserName = user.Identity.Name;
                appUserInfoSetter.UserType = UserType.Authenticated;
                appUserInfoSetter.UserRole = UserRole.Admin;
                string userTokenIdStr = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(userTokenIdStr))
                    appUserInfoSetter.UserTokenId = Guid.Parse(userTokenIdStr);
            }
            else
            {
                appUserInfoSetter.UserName = null;
                appUserInfoSetter.UserType = UserType.Anonymous;
            }
        }

        #endregion Methods
    }
}
