using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Tionit.Enterprise.Exceptions;
using Tionit.Enterprise.Validation;
using Tionit.Persistence;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Admin.Application.Queries.OrderItems.Models;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Admin.Application.Queries.OrderItems
{
    /// <summary>
    /// Возврашяет все позиции заказа
    /// </summary>
    public class GetOrderItemsByOrderIdQuery
    {
        #region Fields

        private readonly IAccessRightChecker accessRightChecker;
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Contructors

        public GetOrderItemsByOrderIdQuery(IAccessRightChecker accessRightChecker 
                                          ,IRepository<OrderItem> orderItemRepository
                                          ,IRepository<Order> orderRepository
                                          ,MapperConfiguration mapperConfiguration)
        {
            this.accessRightChecker = accessRightChecker;
            this.orderItemRepository = orderItemRepository;
            this.orderRepository = orderRepository;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructors

        #region Methods

        public async Task<DataSourceResult> Execute(DataSourceRequest request, Guid orderId)
        {
            // права
            accessRightChecker.CheckIsAdmin();

            if (orderId == Guid.Empty)
                throw new PossibleBugException("Произошла теьническая ощибка");
            
            // достаем товар
            Order order = orderRepository.GetById(orderId).FirstOrDefault();
            Validate.IsNotNull(order).WithErrorMessage("Заказ не найден");

            var a = orderItemRepository.Where(oi => oi.OrderId == orderId);
            var b = a.ProjectTo<OrderItemModel>(mapperConfiguration);
            var c = await b.ToDataSourceResultAsync(request);
            var d = c;
            return d;
        }

        #endregion Methods

    }
}
