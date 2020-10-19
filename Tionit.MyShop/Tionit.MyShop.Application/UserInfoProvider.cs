using System;
using Tionit.Enterprise;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Application
{
    public class UserInfoProvider : IAppUserInfoSetter
    {
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRole UserRole { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName {get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public UserType UserType { get; set; }
     
        /// <summary>
        /// Токен авторизации пользователя
        /// </summary>
        public Guid UserTokenId { get; set; }

        public string UserTechnicalInfo { get; set; }

        public string OriginalUser { get; set; }
    }
}
