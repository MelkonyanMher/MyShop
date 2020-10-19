﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tionit.Enterprise;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : IEntityWithId
    {
        /// <summary>
        /// Уникальный идентификатор(primary key)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Display(Name= "Название")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Display(Name = "Цена")]
        public double Price { get; set; }

        /// <summary>
        /// Позиции
        /// </summary>
        [Display(Name = "Позиции")]
        public ICollection<OrderItem> OrderItems = new HashSet<OrderItem>();
    }
}
