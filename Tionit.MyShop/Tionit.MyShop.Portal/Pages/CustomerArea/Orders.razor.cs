using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Portal.Application.Queries.Orders;
using Tionit.ShopOnline.Portal.Application.Queries.Orders.Models;
using Tionit.ShopOnline.Portal.InteropServices;

namespace Tionit.ShopOnline.Portal.Pages.CustomerArea
{
    public partial class Orders
    {
        #region Inject

        [Inject]
        private Executor<GetOrdersQuery> GetOrdersQueryExecutor { get; set; }

        [Inject]
        private Messages Messages { get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// список моделей заказов
        /// </summary>
        private List<OrderModel> OrderModelList { get; set; } = new List<OrderModel>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// все заказы текущего пользователя
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                OrderModelList = await GetOrdersQueryExecutor.Execute(query => query.Execute());
            }
            catch(BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        #endregion Methods
    }
}
