
using AutoMapper;
using System;
using Tionit.ShopOnline.Domain;
using System.ComponentModel.DataAnnotations;

namespace Tionit.ShopOnline.Portal.Application.Queries.Products.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public int? Quantity { get; set; }

        public bool AddProductToBasketButtonIsVisible => Quantity > 0;

        internal class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Product, ProductModel>();
        }
    }
}
