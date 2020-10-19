using System;
using AutoMapper;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Customers.Models
{
    public class CustomerModel
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
        /// Имя
        /// </summary>
        public string Name { get; set; }

        internal class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Customer, CustomerModel>();
        }
    }
}
