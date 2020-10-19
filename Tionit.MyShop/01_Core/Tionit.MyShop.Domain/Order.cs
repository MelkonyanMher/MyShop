﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tionit.Enterprise;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : IEntityWithId 
    {
        /// <summary>
        /// Уникальный идентификатор(primary key)
        /// </summary>
        public Guid Id { get; set; }

        #region Клиент

        /// <summary>
        /// Id клиента сделавший заказ
        /// </summary>
        [Display(Name = "Id клиента сделавший заказ")]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        [Display(Name = "Клиент")]
        //[Required]
        public Customer Customer { get; set; }

        #endregion

        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Дата создания")]
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        /// Позиции 
        /// </summary>
        [Display(Name = "Позиции")]
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        
        /// <summary>
        /// Адрес доставки
        /// </summary>
        [Display(Name = "Адрес доставки")]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Оформлен?
        /// </summary>
        [Display(Name = "Оформлен?")]
        public bool IsConfirmed { get; set; }
    }
}
