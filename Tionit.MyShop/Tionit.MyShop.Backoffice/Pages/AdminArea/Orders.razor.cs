using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Queries.Orders;
using Tionit.ShopOnline.Backoffice.Application.Queries.Orders.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea
{
    public partial class Orders
    {
        #region Constant

        /// <summary>
        /// Селектор грида
        /// </summary>
        private const string GridCssSelector = ".product-grid";

        #endregion Constant

        #region Inject

        [Inject]
        private Executor<GetAllOrdersQuery> GetAllOrdersQueryExecutor { get; set; }

        [Inject]
        private Messages Messages { get; set; }

        [Inject]
        private Blocker Blocker { get; set; }

        #endregion Inject 

        #region Fields

        /// <summary>
        /// Список заказов
        /// </summary>
        private List<OrderModel> OrderModelList { get; set; }

        /// <summary>
        /// Обшее количество заказов
        /// </summary>
        private int TotalCount { get; set; }

        #endregion Fields

        #region Property

        private DataSourceRequest currentGridRequest;

        #endregion Property

        #region Methods

        /// <summary>
        /// Получение всех заказов
        /// </summary>
        private async Task RebindGrid()
        {
            await Blocker.Block(GridCssSelector);
            try
            {
                var result = await GetAllOrdersQueryExecutor.Execute(query => query.Execute(currentGridRequest));
                OrderModelList = result.Data.Cast<OrderModel>().ToList();
                TotalCount = result.Total;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
            await Blocker.Block(GridCssSelector);
        }

        private async Task OnGridNeedItems(GridReadEventArgs args)
        {
            currentGridRequest = args.Request;
            await RebindGrid();
        }

        /// <summary>
        /// Обновление заказа
        /// </summary>
        private async Task UpdateOrder()
        {
            await Messages.ShowInfo("С перва надо добавить функционал");
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        private async Task RemoveOrder()
        {
            await Messages.ShowInfo("С перва надо добавить функционал");
        }

        #endregion Methods
    }
}
