using System;
using Tionit.Enterprise;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Application.Contract
{
    /// <summary>
    /// Поставщик данных о пользователе системы
    /// </summary>
    public interface IAppUserInfoProvider : IUserInfoProvider
    {
        /// <summary>
        /// Роль пользователя
        /// </summary>
        UserRole UserRole { get; }

        /// <summary>
        /// Id токена авторизации пользователя
        /// </summary>
        Guid UserTokenId { get; set; }
    }
}
