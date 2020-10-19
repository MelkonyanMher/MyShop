﻿using AutoMapper;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Portal.Application.Queries.Baskets.Models
{
    public class ProductModelForBasket
    {
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var mapping = CreateMap<OrderItem, ProductModelForBasket>();
                mapping.ForMember(model => model.Name, opts => opts.MapFrom(oi => oi.Product.Name));
                mapping.ForMember(model => model.Price, opts => opts.MapFrom(oi => oi.Product.Price));
                mapping.ForMember(model => model.Quantity, opts => opts.MapFrom(oi => oi.Quantity));
            }
            
        }
    }
}
