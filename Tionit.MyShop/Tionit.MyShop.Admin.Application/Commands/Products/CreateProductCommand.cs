using System;
using System.Linq;
using System.Threading.Tasks;
using Tionit.AuditLogging;
using Tionit.Enterprise.Validation;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Backoffice.Application.Commands.Products.Models;
using Tionit.ShopOnline.Domain;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Backoffice.Application.Commands.Products
{
    /// <summary>
    /// Добавляем продукт
    /// </summary>
    public class CreateProductCommand
    {
        #region Fields

        private readonly IRepository<Product> productRepository;
        private readonly IChangesSaver changesSaver;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAuditLogger auditLogger;

        #endregion Fields

        #region Constructor

        public CreateProductCommand(IRepository<Product> productRepository, IChangesSaver changesSaver, IAccessRightChecker accessRightChecker,
            IAuditLogger auditLogger)
        {
            this.productRepository = productRepository;
            this.changesSaver = changesSaver;
            this.accessRightChecker = accessRightChecker;
            this.auditLogger = auditLogger;
        }

        #endregion Constructor

        #region Methods

        public async Task<Product> Execute(CreateProductInputModel input)
        {
            //Права доступа
            accessRightChecker.CheckIsAdmin();

            //Предобработка
            input.CheckAndPrepare();

            //Валидация
            Validate.IsNotNullOrWhitespace(input.Name).WithErrorMessage("Название продукта должна быть указана");
            Validate.IsTrue(input.Price > 0).WithErrorMessage("Цена товара должна быть больше нуля");
            Validate.IsNull(productRepository.FirstOrDefault(p=>p.Name.ToLower() == input.Name.Trim().ToLower()))
                                             .WithErrorMessage("Данное название испольуется другим продуктом");

            //Создание продукта
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = input.Name,
                Price = input.Price
            };

            //Добавление продукта в репозиторий
            productRepository.Add(product);

            //Сохранение
            await changesSaver.SaveChangesAsync();

            //Логирование
            await auditLogger
                .Info("Добавление продукта")
                .AppendMessageLine($"Id: {product.Id}")
                .AppendMessageLine($"Название: {product.Name}")
                .AppendMessageLine($"Цена: {product.Price}")
                .RelatedObject(product)
                .LogAsync();

            return product;
        }

        #endregion Methods
    }
}
