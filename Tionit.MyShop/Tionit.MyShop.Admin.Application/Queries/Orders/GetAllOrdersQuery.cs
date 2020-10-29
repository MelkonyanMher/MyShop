using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Domain;
using Tionit.Persistence;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Tionit.MyShop.Admin.Application.Queries.Orders.Models;
using AutoMapper;
using Telerik.DataSource.Extensions;
using Telerik.DataSource;

namespace Tionit.MyShop.Admin.Application.Queries.Orders
{
    /// <summary>
    /// Все заказы
    /// </summary>
    public class GetAllOrdersQuery
    {
        #region Fields

        private readonly IRepository<Order> orderRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetAllOrdersQuery(IRepository<Order> orderRepository, IAccessRightChecker accessRightChecker, MapperConfiguration mapperConfiguration)
        {
            this.orderRepository = orderRepository;
            this.accessRightChecker = accessRightChecker;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Вовращает все заказы
        /// </summary>
        public async Task<DataSourceResult> Execute(DataSourceRequest request)
        {
            //права
            accessRightChecker.CheckIsAdmin();

            //TODO исправить должно возвращать имя клиента и общая стоимость заказа
            return await orderRepository
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .OrderBy(o => o.CreationDateTime).ProjectTo<OrderModel>(mapperConfiguration)
                .ToDataSourceResultAsync(request);
        }

        #endregion Methods
    }
}
