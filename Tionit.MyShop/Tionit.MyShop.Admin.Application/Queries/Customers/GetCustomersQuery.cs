using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Tionit.MyShop.Application.Contract;
using Tionit.MyShop.Admin.Application.Queries.Customers.Models;
using Tionit.MyShop.Domain;
using Tionit.Persistence;

namespace Tionit.MyShop.Admin.Application.Queries.Customers
{
    public class GetCustomersQuery
    {
        #region Fields

        private readonly IAccessRightChecker accessRightChecker;
        private readonly IRepository<Customer> customerRepository;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetCustomersQuery(IAccessRightChecker accessRightChecker, IRepository<Customer> customerRepository, MapperConfiguration mapperConfiguration)
        {
            this.accessRightChecker = accessRightChecker;
            this.customerRepository = customerRepository;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods
        
        /// <summary>
        /// Возвращает всех клиентов
        /// </summary>
        public async Task<DataSourceResult> Execute(DataSourceRequest request)
        {
            //права
            accessRightChecker.CheckIsAdmin();

            return await customerRepository.ProjectTo<CustomerModel>(mapperConfiguration)
                .ToDataSourceResultAsync(request);
        }
        
        #endregion Methods
    }
}
