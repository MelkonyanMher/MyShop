﻿using Tionit.Enterprise.Models;
using Tionit.Enterprise.Models.Attributes;

namespace Tionit.ShopOnline.Backoffice.Application.Commands.Products.Models
{
    /// <summary>
    /// Модель добавление продукта
    /// </summary>
    public class CreateProductInputModel : InputModelBase
    {
        /// <summary>
        /// Название 
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }
    }
}
