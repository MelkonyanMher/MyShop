﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tionit.Enterprise.Validation;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Queries.Orders.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Queries.Orders
{
    /// <summary>
    /// Возвращает все заказы клиента
    /// </summary>
    public class GetOrdersQuery
    {
        #region Fields

        private readonly IAppUserInfoProvider userInfoProvider;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetOrdersQuery(IAppUserInfoProvider userInfoProvider, 
                              IAccessRightChecker accessRightChecker,
                              IRepository<Customer> customerRepository,
                              IRepository<Order> orderRepository,
                              MapperConfiguration mapperConfiguration)
        {
            this.userInfoProvider = userInfoProvider;
            this.accessRightChecker = accessRightChecker;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        public async Task<List<OrderModel>> Execute()
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //находим клиента
            Customer customer = customerRepository.FirstOrDefault(c => c.UserName == userInfoProvider.UserName);
            Validate.IsNotNull(customer).WithErrorMessage("Сотрудник не найден");

            //возвращаем список его заказов
            return await orderRepository.Include(o=>o.OrderItems).Where(o => o.CustomerId == customer.Id).ProjectTo<OrderModel>(mapperConfiguration).ToListAsync();
        }

        #endregion Methods
    }
}
