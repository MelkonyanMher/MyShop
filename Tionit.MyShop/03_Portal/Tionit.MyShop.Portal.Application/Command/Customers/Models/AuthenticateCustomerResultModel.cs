﻿using System;
using System.Collections.Generic;
using System.Text;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Portal.Application.Command.Customers.Models
{
    public class AuthenticateCustomerResultModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string AccessToken { get; set; }
    }
}
