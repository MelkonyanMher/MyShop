﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Portal.Application.Queries.Products.Models
{
    public class ProductViewModelById
    {
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
            public MappingProfile() => CreateMap<Product, ProductViewModelById>();
        }
    }
}
