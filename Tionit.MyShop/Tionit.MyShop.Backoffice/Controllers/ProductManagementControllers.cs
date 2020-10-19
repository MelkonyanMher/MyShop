using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telerik.DataSource;
using Tionit.ShopOnline.Backoffice.Application.Commands.Products;
using Tionit.ShopOnline.Backoffice.Application.Commands.Products.Models;
using Tionit.ShopOnline.Backoffice.Application.Queries.Produts;
using Tionit.ShopOnline.Backoffice.DataRequests;
using Tionit.ShopOnline.Domain;


namespace Tionit.ShopOnline.Backoffice.Controllers
{
    [Route("api/[controller]")]
    public class ProductManagementControllers : Controller
    {
        #region Fields

        private readonly CreateProductCommand createProductCommand;
        private readonly GetProductsQuery getProductsQuery;
        
        #endregion Fields

        #region Constructor

        public ProductManagementControllers(CreateProductCommand createProductCommand, GetProductsQuery getProductsQuery)
        {
            this.createProductCommand = createProductCommand;
            this.getProductsQuery = getProductsQuery;
        }

        #endregion Constructor

        /// <summary>
        /// Возвращает список продуктов
        /// </summary>
        [HttpGet("getProduct")]
        public async Task<DataSourceResult> GetProducts([FromBody]DataSourceRequest request)
        {
            return await getProductsQuery.Execute(request);
        }

        /// <summary>
        /// Создаем продукт
        /// </summary>

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<Product> CreateProduct([DataRequest]CreateProductInputModel inputModel)
        {
            return await createProductCommand.Execute(inputModel);
        }
    }
}
