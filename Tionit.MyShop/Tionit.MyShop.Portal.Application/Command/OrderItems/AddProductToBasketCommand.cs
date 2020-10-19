using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tionit.AuditLogging;
using Tionit.Enterprise.Exceptions;
using Tionit.Enterprise.Validation;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Command.OrderItems.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Command.OrderItems
{
    /// <summary>
    /// Добавление продукта в корзину
    /// </summary>
    public class AddProductToBasketCommand
    {
        #region Fields

        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAppUserInfoProvider userInfoProvider;
        private readonly IRepository<Customer> customerRepository;
        private readonly IChangesSaver changesSaver;
        private readonly IAuditLogger auditLogger;
        private readonly IRepository<Order> orderRepository;

        #endregion Fields

        #region Constructor

        public AddProductToBasketCommand(IRepository<OrderItem> orderItemRepository, IRepository<Product> productRepository,
                                  IAccessRightChecker accessRightChecker, IAppUserInfoProvider userInfoProvider,
                                  IRepository<Customer> customerRepository, IChangesSaver changesSaver, IAuditLogger auditLogger, IRepository<Order> orderRepository)
        {
            this.orderItemRepository = orderItemRepository;
            this.productRepository = productRepository;
            this.accessRightChecker = accessRightChecker;
            this.userInfoProvider = userInfoProvider;
            this.customerRepository = customerRepository;
            this.changesSaver = changesSaver;
            this.auditLogger = auditLogger;
            this.orderRepository = orderRepository;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// добавляет товар в корзину
        /// </summary>
        public async Task Execute(AddProductToBasketInputModel input)
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //предобработка
            input.CheckAndPrepare();

            //Валидации
            Product product = productRepository.FirstOrDefault(p => p.Id == input.Id);
            if (product == null)
                throw new NotFoundException("Товар не найден");
            Customer customer = customerRepository.FirstOrDefault(c => c.UserName == userInfoProvider.UserName);
                if (customer == null)
                throw new NotFoundException("Клиент не найден");
            Validate.IsTrue(input.Quantity > 0).WithErrorMessage("Количество заказа должно быть больше нуля");

            OrderItem orderItem = null;

            //ищем неоформленный заказ
            Order order = orderRepository.Include(oi=>oi.OrderItems).FirstOrDefault(o => !o.IsConfirmed && o.CustomerId == customer.Id);

            //если такой заказ есть
            if (order != null)
            {
                //ищем указанный товар в корзине 
                orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductId == input.Id);
                //если товар найден добавляем количество
                if (orderItem != null)
                {
                    orderItem.Quantity += input.Quantity;
                }
                //иначе создаем позицию заказа
                else
                {
                    orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Quantity = input.Quantity
                    };
                    orderItem.OrderId = order.Id;
                    
                    //добавление в репозторию
                    orderItemRepository.Add(orderItem);
                }
            }
            else //если нету неоформленного заказа
            {
                order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id
                };

                orderRepository.Add(order);
                orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = input.Quantity
                };
               
                //добавление в репозторию
                orderItemRepository.Add(orderItem);
            }            

            //сохранение
            await changesSaver.SaveChangesAsync();

            //лог
            await auditLogger
                .Info($"Добавлена новая позиция заказа: {orderItem.Id}")
                .AppendMessageLine($"Id заказа: {order.Id}")
                .AppendMessageLine($"Id продукта: {product.Id}")
                .RelatedObject(orderItem)
                .LogAsync();
        }

        #endregion Methods
    }
}
