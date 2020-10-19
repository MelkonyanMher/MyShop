using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tionit.AuditLogging;
using Tionit.Enterprise;
using Tionit.Enterprise.Exceptions;
using Tionit.Enterprise.Validation;
using Tionit.Net.Email;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Application.Contract.Infrastructure.MessageFormer;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Command.Orders.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Command.Orders
{
    /// <summary>
    /// Создание заказа
    /// </summary>
    public class CreateOrderCommand
    {
        #region Fields

        private readonly IUserInfoProvider userInfoProvider;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IRepository<Customer> customerRepository;
        private readonly IChangesSaver changesSaver;
        private readonly IAuditLogger auditLogger;
        private readonly IRepository<Order> orderRepository;
        private readonly IDateTimeProvider dateTime;
        private readonly IOrderCreationNotificationMessageFormer orderCreationNotificationMessageFormer;
        private readonly IEmailEnqueuer emailEnqueuer;
        private readonly IRepository<Administrator> administratorRepository;

        #endregion Fields

        #region Constructor

        public CreateOrderCommand(IUserInfoProvider userInfoProvider, IAccessRightChecker accessRightChecker, IRepository<Customer> customerRepository,
                                  IChangesSaver changesSaver, IAuditLogger auditLogger, IRepository<Order> orderRepository, IDateTimeProvider dateTime,
                                  IOrderCreationNotificationMessageFormer orderCreationNotificationMessageFormer, IEmailEnqueuer emailEnqueuer,
                                  IRepository<Administrator> administratorRepository)
        {
            this.userInfoProvider = userInfoProvider;
            this.accessRightChecker = accessRightChecker;
            this.customerRepository = customerRepository;
            this.changesSaver = changesSaver;
            this.auditLogger = auditLogger;
            this.orderRepository = orderRepository;
            this.dateTime = dateTime;
            this.orderCreationNotificationMessageFormer = orderCreationNotificationMessageFormer;
            this.emailEnqueuer = emailEnqueuer;
            this.administratorRepository = administratorRepository;
        }

        #endregion Constructor

        #region Methods

        public async Task Execute(CreateOrderInputModel input)
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //предобработка
            input.CheckAndPrepare();

            //валидации
            Validate.IsNotNullOrWhitespace(input.Address).WithErrorMessage("Адрес доставки должен быть указан");
            
            //Находим клиента
            Customer customer = customerRepository.FirstOrDefault(c => c.UserName == userInfoProvider.UserName);
            if (customer == null)
                throw new NotFoundException("Клиент не найден");

            //Находим неоформленный заказ
            Order order = orderRepository.FirstOrDefault(o => !o.IsConfirmed && o.CustomerId == customer.Id);
            if(order == null)
                throw new NotFoundException("В корзине нет продуктов");

            //добавление заказа
            order.IsConfirmed = true;
            order.DeliveryAddress = input.Address;
            order.CreationDateTime = dateTime.Now;

            //сохранение заказа
            await changesSaver.SaveChangesAsync();

            //лог
            await auditLogger
                .Info($"Оформлен новый заказ {order.Id}")
                .AppendMessageLine($"Id: {order.Id}")
                .AppendMessageLine($"клиент: {customer.UserName}")
                .RelatedObject(order)
                .LogAsync();

            //Общая стоимость заказа
            double totalSum = order.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price);
            
            //текст письма при оформлении заказа
            string messageText = orderCreationNotificationMessageFormer.Form(customer.Name, $"{totalSum}", "Название заказа");

            var administratorsEmail = administratorRepository.Select(a => a.Email);
            await emailEnqueuer
                .To(administratorsEmail)
                .SendingMode(SendingMode.Individual)
                .Subject("Создание заказа в ShopOnline")
                .Body(messageText)
                .BodyFormat(EmailMessageBodyFormat.Html)
                .EnqueueAsync();
        }

        #endregion Methods
    }
}
