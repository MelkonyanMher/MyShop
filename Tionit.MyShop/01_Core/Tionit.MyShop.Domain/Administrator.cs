﻿using System;
using System.ComponentModel.DataAnnotations;
using Tionit.Enterprise;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Администратор
    /// </summary>
    public class Administrator: IEntityWithId
    {
        /// <summary>
        /// Уникальный идентификатор(primary key)
        /// </summary>
        public Guid Id { get; set; }

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
        /// E-mail
        /// </summary>
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public Guid AuthTokenId { get; set; }
    }
}
