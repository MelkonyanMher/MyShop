using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tionit.AuditLogging;
using Tionit.Enterprise.Validation;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Application.Utilities;
using Tionit.MyShop.Backoffice.Application.Commands.Customers.Models;
using Tionit.MyShop.Domain;
using Tionit.Persistence;

namespace Tionit.MyShop.Backoffice.Application.Commands.Customers
{
    public class CreateCustomerCommand
    {
        #region Fields

        private readonly IRepository<Customer> customerRepository;
        private readonly IChangesSaver changesSaver;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAuditLogger auditLogger;

        #endregion Fields

        #region Constructor

        public CreateCustomerCommand(IRepository<Customer> customerRepository, IChangesSaver changesSaver, IAccessRightChecker accessRightChecker,
                                     IAuditLogger auditLogger)
        {
            this.changesSaver = changesSaver;
            this.customerRepository = customerRepository;
            this.accessRightChecker = accessRightChecker;
            this.auditLogger = auditLogger;
        }

        #endregion Constructor

        #region Methods

        public async Task<string> Execute(CreateCustomerInputModel input)
        {
            //права
            accessRightChecker.CheckIsAdmin();

            //предобработка
            input.CheckAndPrepare();

            //валидация
            Validate.IsNotNullOrWhitespace(input.UserName).WithErrorMessage("Логин должен быть указан");
            Validate.IsFalse(await customerRepository.AnyAsync(c => c.UserName == input.UserName.ToLower()))
                                              .WithErrorMessage("Логин используется другим пользователем");
            Validate.IsNotNullOrWhitespace(input.Name).WithErrorMessage("Имя сотрудника должно быть указано");

            //Пароьль клиента
            string password = PasswordGenerator.GeneratorPassword(6);

            //коздаем клиента
            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                AuthTokenId = Guid.NewGuid(),
                Name = input.Name,
                PasswordHash = HashHelper.GetHashString(password),
                UserName = input.UserName.ToLower()
            };

            //добавление клиента в репозиторий
            customerRepository.Add(customer);

            //сохранение клиента в базе
            await changesSaver.SaveChangesAsync();

            //лог
            await auditLogger
                .Info("Добавлен новый клиент")
                .AppendMessageLine($"Id: {customer.Id}")
                .AppendMessageLine($"Имя: {customer.Name}")
                .AppendMessageLine($"Логин: {customer.UserName}")
                .RelatedObject(customer)
                .LogAsync();

            return password;
        }

        #endregion Methods
    }
}
