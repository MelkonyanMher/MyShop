﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Queries.Baskets.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Queries.Baskets
{
    public class GetProductsFromBasketQuery
    {
        #region Fields

        private readonly IAppUserInfoProvider userInfoProvider;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IRepository<Order> orderRepository;
        private readonly MapperConfiguration mapperConfiguration;
        #endregion Fields

        #region Constructor

        public GetProductsFromBasketQuery(IAppUserInfoProvider userInfoProvider, IAccessRightChecker accessRightChecker, IRepository<Order> orderRepository,
                                     MapperConfiguration mapperConfiguration)
        {
            this.userInfoProvider = userInfoProvider;
            this.accessRightChecker = accessRightChecker;
            this.orderRepository = orderRepository;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Возвращает продукты которые в корзине
        /// </summary>
        public async Task<List<ProductModelForBasket>> Execute()
        {
            //Права
            accessRightChecker.CheckIsCustomer();

            //возвращает продукты в из корзины
            return await orderRepository.Include(o => o.OrderItems).ThenInclude(o => o.Product)
                .Where(o => !o.IsConfirmed && userInfoProvider.UserName == o.Customer.UserName)
                .SelectMany(o => o.OrderItems).ProjectTo<ProductModelForBasket>(mapperConfiguration).ToListAsync();
        }

        #endregion Methods

    }
}
