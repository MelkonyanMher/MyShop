using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tionit.ShopOnline.Portal.Application.Queries.Products;
using Tionit.ShopOnline.Portal.Application.Queries.Products.Models;
using Tionit.ShopOnline.Portal.InteropServices;
using System.ComponentModel.DataAnnotations;
using System;
using Tionit.ShopOnline.Portal.Application.Command.OrderItems.Models;
using Tionit.ShopOnline.Portal.Application.Command.OrderItems;
using Tionit.Enterprise.Exceptions;

namespace Tionit.ShopOnline.Portal.Pages.CustomerArea
{
    public partial class Products
    {
        #region Const

        /// <summary>
        /// Добавляемое количесто товара при нажатии кнопки "Показать еще"
        /// </summary>
        private const int AddingProductCount = 2;

        #endregion Const

        #region Inject

        [Inject]
        public Executor<GetAllProductsQuery> GetAllProductsQueryExecutor { get; set; }

        [Inject]
        public Executor<AddProductToBasketCommand> AddProductToBasketCommandExecutor { get; set; }

        [Inject]
        public Messages Messages { get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// Общее количество продуктов в базе
        /// </summary>
        private int Total { get; set; }

        /// <summary>
        /// Список продуктов
        /// </summary>
        private List<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Показать кнопку добавления товаров
        /// </summary>
        private bool ShowAddButton => Total <= ProductList.Count;
               
        /// <summary>
        /// Изначально получаем первые 3 продукта
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var productViewModel = await GetAllProductsQueryExecutor.Execute(command => command.Execute(0, 3));
            ProductList = productViewModel.productModel;
            Total = productViewModel.Total;
        }

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        private async Task AddProductToBasket(ProductModel product)
        {
            try
            {
                var input = new AddProductToBasketInputModel
                {
                    Id = product.Id,
                    Quantity = product.Quantity.Value
                };
                await AddProductToBasketCommandExecutor.Execute(commad => commad.Execute(input));
                await Messages.ShowInfo("Продукт успешно добавлен в корзину");
                product.Quantity = null;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        /// <summary>
        /// Получаем из базы данных новую партию товаров
        /// </summary>
        /// <returns></returns>
        private async Task AddProductsCount()
        {
            var productsViewModel = await GetAllProductsQueryExecutor.Execute(command => command.Execute(ProductList.Count, AddingProductCount));
            var list = productsViewModel.productModel;
            Total = productsViewModel.Total;
            ProductList.AddRange(list);
        }

        #endregion Methods        
    }
}
