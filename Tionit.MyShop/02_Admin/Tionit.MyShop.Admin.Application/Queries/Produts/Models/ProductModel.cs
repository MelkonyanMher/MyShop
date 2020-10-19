﻿using System;
using AutoMapper;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Produts.Models
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

        internal class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Product, ProductModel>();
        }
    }
}
