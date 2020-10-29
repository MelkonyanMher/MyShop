using System;
using AutoMapper;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Admin.Application.Queries.Customers.Models
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
