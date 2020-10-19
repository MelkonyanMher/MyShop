using System;
using Tionit.Enterprise;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Application.Contract
{
    public interface IAppUserInfoSetter : IAppUserInfoProvider
    {
        /// <summary>
        /// Имя пользоваеля
        /// </summary>
        new string UserName { get; set; }

        /// <summary>
        /// Техническая информация
        /// </summary>
        new string UserTechnicalInfo { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        new UserType UserType { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        new UserRole UserRole { get; set; }

        /// <summary>
        /// Id токена авторизации пользователя
        /// </summary>
        new Guid UserTokenId { get; set; }
    }
}
