using System;
using Tionit.Enterprise;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Application
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
