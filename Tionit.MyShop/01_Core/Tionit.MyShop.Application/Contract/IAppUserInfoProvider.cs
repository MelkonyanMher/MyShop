﻿using System;
using Tionit.Enterprise;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Application.Contract
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
