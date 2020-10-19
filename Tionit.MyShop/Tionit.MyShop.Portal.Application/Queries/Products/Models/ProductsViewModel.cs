using System.Collections.Generic;

namespace Tionit.MyShop.Portal.Application.Queries.Products.Models
{
    public class ProductsViewModel
    {
        /// <summary>
        /// Модель продукта
        /// </summary>
        public List<ProductModel> productModel { get; set; }

        /// <summary>
        /// Общее количество продуктов в базе
        /// </summary>
        public int Total { set; get; }
    }
}
