using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Queries.OrderItems;
using Tionit.ShopOnline.Backoffice.Application.Queries.OrderItems.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea
{
    public partial class OrderItems
    {
        #region Constrant

        /// <summary>
        /// Селектор грида
        /// </summary>
        private const string GridCssSelector = ".order-item-grid";
        
        #endregion Constrant

        #region Inject

        [Inject]
        private Executor<GetOrderItemsByOrderIdQuery> GetOrderItemsByOrderIdQueryExecutor { get; set; }

        [Inject]
        private Messages Messages { get; set; }

        [Inject]
        private Blocker Blocker { get; set; }

        #endregion Inject

        #region Patameter

        /// <summary>
        /// Id заказа
        /// </summary>
        [Parameter]
        public Guid OrderId { get; set; }

        #endregion Parameter

        #region Property

        private DataSourceRequest currentGridRequest;

        /// <summary>
        /// Количество позиции заказа
        /// </summary>
        private int TotalCount { get; set; }

        #endregion Property

        #region Fields

        /// <summary>
        /// список позиций заказа
        /// </summary>
        private List<OrderItemModel> OrderItemModelList { get; set; } = new List<OrderItemModel>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// отображает грид
        /// </summary>
        private async Task OnGridNeedItems(GridReadEventArgs args)
        {
            currentGridRequest = args.Request;
            await RebindGrid();
        }

        /// <summary>
        /// обнобляеят грид
        /// </summary>
        /// <returns></returns>
        private async Task RebindGrid()
        {
            await Blocker.Block(GridCssSelector);

            try
            {
                var result = await GetOrderItemsByOrderIdQueryExecutor.Execute(query => query.Execute(currentGridRequest, OrderId));
                OrderItemModelList = result.Data.Cast<OrderItemModel>().ToList();
                TotalCount = result.Total;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }

            await Blocker.Unblock(GridCssSelector);
        }
       
        #endregion Methods
    }
}
