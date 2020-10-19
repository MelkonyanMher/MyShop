using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tionit.Enterprise.Models;

namespace Tionit.MyShop.Portal.Application.Command.OrderItems.Models
{
    public class AddProductToBasketInputModel : InputModelBase
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        [NotNull]
        public Guid Id { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }
    }
}
