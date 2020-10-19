using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tionit.Enterprise.Exceptions;
using Tionit.Enterprise.Validation;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Portal.Application.Queries.Products.Models;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Portal.Application.Queries.Products
{
    /// <summary>
    /// Продукт по Id
    /// </summary>
    public class GetProductForViewByIdQuery
    {
        #region Fields

        private readonly IRepository<Product> productRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetProductForViewByIdQuery(IRepository<Product> productRepository, IAccessRightChecker accessRightChecker, MapperConfiguration mapperConfiguration)
        {
            this.productRepository = productRepository;
            this.accessRightChecker = accessRightChecker;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Возвращает модель продукта по Id для просмотра
        /// </summary>
        public async Task<ProductViewModelById> Execute(Guid Id)
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //если не указан Id
            if(Id == Guid.Empty)
                throw new PossibleBugException("Указан некорректный Id");

            var productForView = await productRepository.GetById(Id).ProjectTo<ProductViewModelById>(mapperConfiguration).FirstOrDefaultAsync();

            //если продукт не найден
            if (productForView == null)
                throw new NotFoundException("Продукт не найден");
            
            return productForView;
        }

        #endregion Methods
    }
}
