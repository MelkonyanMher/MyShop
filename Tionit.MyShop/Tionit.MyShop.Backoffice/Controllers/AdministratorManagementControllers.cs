using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telerik.DataSource;
using Tionit.ShopOnline.Backoffice.Application.Commands;
using Tionit.ShopOnline.Backoffice.Application.Commands.Administrators;
using Tionit.ShopOnline.Backoffice.Application.Commands.Administrators.Models;
using Tionit.ShopOnline.Backoffice.Application.Queries.Administrators;
using Tionit.ShopOnline.Backoffice.DataRequests;

namespace Tionit.ShopOnline.Backoffice.Controllers
{
    [Route("api/[controller]")]
    public class AdministratorManagementControllers : Controller
    {
        #region Fields

        private readonly AuthenticateAdministratorCommand authenticateAdministratorCommand;
        private readonly ChangeAdministratorPasswordCommand changeAdministratorPasswordCommand;
        private readonly GetAdministratorsQuery getAdministratorsQuery;
        
        #endregion Fields

        #region Constructor

        public AdministratorManagementControllers(AuthenticateAdministratorCommand authenticateAdministratorCommand,
                                                  ChangeAdministratorPasswordCommand changeAdministratorPasswordCommand,
                                                  GetAdministratorsQuery getAdministratorsQuery)
        {
            this.authenticateAdministratorCommand = authenticateAdministratorCommand;
            this.changeAdministratorPasswordCommand = changeAdministratorPasswordCommand;
            this.getAdministratorsQuery = getAdministratorsQuery;
        }

        #endregion Constructor

        #region Methods
        
        /// <summary>
        /// Аутентификцаия администратора
        /// </summary>
        [HttpPost("AuthenticateAdministrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateAdministratorResultModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthenticateAdministratorResultModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<AuthenticateAdministratorResultModel> AuthenticateAdministrator([FromBody]AuthenticateAdministratorInputModel inputModel)
        {
            return await authenticateAdministratorCommand.Execute(inputModel);
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<AuthenticateAdministratorResultModel> ChangePassword([FromBody]ChangeAdministratorPasswordInputModel input)
        {
            return await changeAdministratorPasswordCommand.Execute(input);
        }

        [HttpGet("GetAdministrators")]
        [Authorize]
        public async Task<DataSourceResult> GetAdministrators([DataRequest]DataSourceRequest request)
        {
            return await getAdministratorsQuery.Execute(request);
        }

        #endregion Methods
    }
}
