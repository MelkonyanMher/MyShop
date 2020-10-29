using AutoMapper;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Admin.Application.Queries.Orders.Models
{
    public class OrderItemModel
    {
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        internal class MappingProfile: Profile
        {
            public MappingProfile()
            {
                var mapping = CreateMap<OrderItem, OrderItemModel>();
                mapping.ForMember(model => model.Name, opts => opts.MapFrom(oi => oi.Product.Name));
                mapping.ForMember(model => model.Price, opts => opts.MapFrom(oi => oi.Product.Price));
                mapping.ForMember(model => model.Quantity, opts => opts.MapFrom(oi => oi.Quantity));
            }
        }
    }
}
