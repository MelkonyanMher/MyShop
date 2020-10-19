using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Commands.Products;
using Tionit.ShopOnline.Backoffice.Application.Commands.Products.Models;
using Tionit.ShopOnline.Backoffice.Application.Queries.Produts;
using Tionit.ShopOnline.Backoffice.Application.Queries.Produts.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telerik.Blazor.Components;
using Telerik.DataSource;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea
{
    public partial class Products
    {
        #region Constant

        /// <summary>
        /// Селектор грида
        /// </summary>
        private const string GridCssSelector = ".product-grid";

        #endregion Constant

        #region Inject

        [Inject]
        public Blocker Blocker { get; set; }

        [Inject]
        public Messages Messages { get; set; }

        [Inject]
        public Executor<GetProductsQuery> GetProductsQueryExecutor { get; set; }

        [Inject]
        public Executor<CreateProductCommand> CreateProductCommandExecutor { get; set; }

        #endregion Inject

        #region Properties

        /// <summary>
        /// Список продуктов
        /// </summary>
        private IEnumerable<ProductModel> ProductList { get; set; }

        /// <summary>
        /// Количество продуктов
        /// </summary>
        private int TotalCount { get; set; }

        /// <summary>
        /// Показать или отключить окно авторизации
        /// </summary>
        private bool IsCreateMode { get; set; }

        /// <summary>
        /// Модель продукта которая отображается в UI
        /// </summary>
        private readonly CreateProductUiModel createProductUiModel = new CreateProductUiModel();

        private DataSourceRequest currentGridRequest;

        #endregion Properties


        #region Methods

        /// <summary>
        /// Открывает окно добавления продукта
        /// </summary>
        private void ShowCreateWindow()
        {
            IsCreateMode = true;
        }

        /// <summary>
        /// Закрывает окно добавления продукта
        /// </summary>
        private void CloseCreateWindow()
        {
            IsCreateMode = false;
            createProductUiModel.Name = null;
            createProductUiModel.Price = null;
        }

        /// <summary>
        /// Возврашяет продукты
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task OnGridNeedItems(GridReadEventArgs args)
        {
            currentGridRequest = args.Request;
            await RebindGrid();
        }

        /// <summary>
        /// Обновляет грид
        /// </summary>
        private async Task RebindGrid()
        {
            await Blocker.Block(GridCssSelector);
            try
            {
                var result = await GetProductsQueryExecutor.Execute(query => query.Execute(currentGridRequest));
                ProductList = result.Data.Cast<ProductModel>();
                TotalCount = result.Total;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
            await Blocker.Unblock(GridCssSelector);
        }
        /// <summary>
        /// Показать кнопку сохранения
        /// </summary>
        private bool IsSaveButtonEnabled =>
            !string.IsNullOrEmpty(createProductUiModel.Name) &&
            createProductUiModel.Price != null;
        
        /// <summary>
        /// Обновление продукта
        /// </summary>
        private async Task UpdateProduct()
        {
            await Messages.ShowInfo("Необходимо добавить функционал");
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        private async Task RemoveProduct()
        {
            await Messages.ShowInfo("Необходимо добавить функционал");
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        private async Task CreateProduct()
        {
            try
            {
                CreateProductInputModel input = null;
                if (createProductUiModel.Price.HasValue)
                {
                    input = new CreateProductInputModel
                    {
                        Name = createProductUiModel.Name,
                        Price = createProductUiModel.Price.Value
                    };

                    await CreateProductCommandExecutor.Execute(command => command.Execute(input));

                    await Messages.ShowInfo("Новый продукт успешно добавлен");

                    CloseCreateWindow();

                    await RebindGrid();
                }
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        #endregion Methods

        #region CreateProductUiModel class

        /// <summary>
        /// Модель продукта которая отображается в UI
        /// </summary>
        public class CreateProductUiModel
        {
            /// <summary>
            /// название продукта
            /// </summary>
            [Required(ErrorMessage = "Название должно быть указано")]
            public string Name { get; set; }

            /// <summary>
            /// Цена
            /// </summary>
            [Required(ErrorMessage = "Цена должна быть указано")]
            [Range(0, double.MaxValue, ErrorMessage = "Цена товара должна быть положительным числом")]
            public double? Price { get; set; }
        }

        #endregion CreateProductUiModel class
    }
}
