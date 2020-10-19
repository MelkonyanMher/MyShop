﻿using System;
using System.ComponentModel.DataAnnotations;
using Tionit.Enterprise;

namespace Tionit.ShopOnline.Domain
{
    /// <summary>
    /// Позиция заказа
    /// </summary>
    public class OrderItem : IEntityWithId
    {
        /// <summary>
        /// Уникальный идентификатор(primary key)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        #region Товар

        /// <summary>
        /// Id связанной сущности "Товар"
        /// </summary>
        [Display(Name = "Id связанной сущности Товар")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        [Display(Name = "Товар")]
        [Required]
        public Product Product { get; set; }

        #endregion Товар

        #region Заказ

        /// <summary>
        /// Id связанной сущности "Заказ"
        /// </summary>
        [Display(Name = "Id связанной сущности Заказ")]
        public Guid OrderId { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        [Display(Name = "Заказ")]
        [Required]
        public Order Order { get; set; }

        #endregion Заказ
    }
}
