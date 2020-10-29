using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Admin.Application.Queries.Produts.Models;
using Tionit.MyShop.Domain;
using Tionit.Persistence;

namespace Tionit.MyShop.Admin.Application.Queries.Produts
{
    public class GetProductsQuery
    {
        #region Fields

        private readonly IRepository<Product> productRepository;
        private readonly IAccessRightChecker accessRightChecker;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetProductsQuery(IRepository<Product> productRepository, IAccessRightChecker accessRightChecker, MapperConfiguration mapperConfiguration)
        {
            this.productRepository = productRepository;
            this.accessRightChecker = accessRightChecker;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        public async Task<DataSourceResult> Execute(DataSourceRequest request)
        {
            //Права
            accessRightChecker.CheckIsAdmin();

            return await productRepository.ProjectTo<ProductModel>(mapperConfiguration)
                .ToDataSourceResultAsync(request);
        }
        #endregion Methods
    }
}
