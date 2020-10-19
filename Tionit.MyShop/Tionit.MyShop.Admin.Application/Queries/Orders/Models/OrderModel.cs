using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Orders.Models
{
    public class OrderModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public double TotalSum { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        /// Позиции заказа
        /// </summary>
        public List<OrderItemModel> OrderItemModelList { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var mapping = CreateMap<Order, OrderModel>();
                mapping.ForMember(model => model.CustomerName, opts => opts.MapFrom(o => o.Customer.Name));
                mapping.ForMember(model => model.OrderItemModelList, opts => opts.MapFrom(o => o.OrderItems));
                mapping.ForMember(model => model.TotalSum, opts => opts.MapFrom(o => Math.Round(o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity), 2)));
                mapping.ForMember(model => model.DeliveryAddress, opts => opts.MapFrom(o => o.DeliveryAddress));
                mapping.ForMember(model => model.CreationDateTime, opts => opts.MapFrom(o => o.CreationDateTime));
                mapping.ForMember(model => model.Id, opts => opts.MapFrom(o => o.Id));
            }
        }
    }
}
