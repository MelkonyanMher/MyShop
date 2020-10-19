using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Domain;
using Tionit.MyShop.Portal.Application.Queries.Products.Models;
using Tionit.Persistence;

namespace Tionit.MyShop.Portal.Application.Queries.Products
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllProductsQuery
    {
        #region Fields

        private readonly IRepository<Product> productRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly MapperConfiguration mapperConfiguration;
        
        #endregion Fields

        #region Constructor

        public GetAllProductsQuery(IRepository<Product> productRepository, IAccessRightChecker accessRightChecker, MapperConfiguration mapperConfiguration)
        {
            this.productRepository = productRepository;
            this.accessRightChecker = accessRightChecker;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Возвращает все продукты
        /// </summary>
        public async Task<List<ProductModel>> Execute()
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //возвращает все продукты
            return await productRepository.ProjectTo<ProductModel>(mapperConfiguration).ToListAsync();
        }

        /// <summary>
        /// Возвращает елементы начиная с позиции skip + 1 в количестве take штук
        /// </summary>
        public async Task<ProductsViewModel> Execute(int skip, int take)
        {
            //права
            accessRightChecker.CheckIsCustomer();

            //возвращает елементы начиная с позиции skip + 1 в количестве take штук
            List<ProductModel> productModel =  await productRepository.Skip(skip).Take(take)
                .ProjectTo<ProductModel>(mapperConfiguration).ToListAsync();

            ProductsViewModel productsViewModel = new ProductsViewModel
            {
                productModel = productModel,
                Total = productRepository.Count()
            };

            return productsViewModel;
        }
        #endregion Methods
    }
}
