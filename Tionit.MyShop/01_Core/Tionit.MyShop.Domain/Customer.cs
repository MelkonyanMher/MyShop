﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tionit.Enterprise;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Customer : IEntityWithId
    {
        /// <summary>
        /// Уникальный идентификатор(primary key)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public Guid AuthTokenId { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Display(Name = "Логин")]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        [Display(Name = "Хеш пароля")]
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        [Display(Name = "Заказы")]
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
