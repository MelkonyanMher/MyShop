﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Portal.Application.Queries.Orders.Models
{
    /// <summary>
    /// Модель заказа
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Позиции заказа
        /// </summary>
        public List<OrderItemModel> OrderItemModelList { get; set; }

        /// <summary>
        /// Цена заказа
        /// </summary>
        public double TotalSum => OrderItemModelList.Sum(oi => oi.Price * oi.Quantity);

        /// <summary>
        /// Адрес доставки
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// дата создания заказа
        /// </summary>
        public DateTime CreationDateTime { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var mappingExpression = CreateMap<Order, OrderModel>();
                mappingExpression.ForMember(e => e.OrderItemModelList, s => s.MapFrom(o => o.OrderItems));
            }
        }
    }
}
