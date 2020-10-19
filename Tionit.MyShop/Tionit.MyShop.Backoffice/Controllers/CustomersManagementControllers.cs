using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telerik.DataSource;
using Tionit.ShopOnline.Backoffice.Application.Commands.Customers;
using Tionit.ShopOnline.Backoffice.Application.Commands.Customers.Models;
using Tionit.ShopOnline.Backoffice.Application.Queries.Customers;
using Tionit.ShopOnline.Backoffice.DataRequests;


namespace Tionit.ShopOnline.Backoffice.Controllers
{
    [Route("api/[controller]")]
    public class CustomersManagementControllers : Controller
    {
        #region Fields

        private readonly CreateCustomerCommand createCustomerCommand;
        private readonly GetCustomersQuery getCustomersQuery;
        
        #endregion Fields

        #region Constructor

        public CustomersManagementControllers(CreateCustomerCommand createCustomerCommand, GetCustomersQuery getCustomersQuery)
        {
            this.createCustomerCommand = createCustomerCommand;
            this.getCustomersQuery = getCustomersQuery;
        }

        #endregion Constructor

        #region Methods
        
        [HttpPost("createCustomer")]
        public async Task<string> CreateCustomer([FromBody] CreateCustomerInputModel inputModel)
        {
            return await createCustomerCommand.Execute(inputModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<DataSourceResult> GetCustomers([DataRequest] DataSourceRequest request)
        {
            return await getCustomersQuery.Execute(request);
        }

        #endregion Methods
    }
}
