﻿using System;
using AutoMapper;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Administrators.Models
{
    /// <summary>
    /// Модель администратора
    /// </summary>
    public class AdministratorModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
       
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Administrator, AdministratorModel>();
        }
    }
}