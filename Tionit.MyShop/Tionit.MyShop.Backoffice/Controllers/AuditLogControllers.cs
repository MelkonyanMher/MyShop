using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telerik.DataSource;
using Tionit.ShopOnline.Backoffice.Application.Queries.Log;
using Tionit.ShopOnline.Backoffice.DataRequests;

namespace Tionit.ShopOnline.Backoffice.Controllers
{
    [Route("api/[controller]")]
    public class AuditLogControllers : Controller
    {
        #region Fields

        private readonly GetLogEntriesQuery getLogEntriesQuery;

        #endregion Fields

        #region Constructor

        public AuditLogControllers(GetLogEntriesQuery getLogEntriesQuery)
        {
            this.getLogEntriesQuery = getLogEntriesQuery;
        }

        #endregion Constructor

        #region Methods

        [HttpGet("GetLogEntries")]
        [Authorize]
        public async Task<DataSourceResult> GetLogEntries([DataRequest]DataSourceRequest request)
        {
            return await getLogEntriesQuery.Execute(request);
        }

        #endregion Methods
    }
}
