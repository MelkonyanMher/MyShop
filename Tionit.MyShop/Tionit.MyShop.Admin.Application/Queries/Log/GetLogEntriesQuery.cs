using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using Tionit.AuditLogging;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Backoffice.Application.Queries.Log.Models;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace Tionit.ShopOnline.Backoffice.Application.Queries.Log
{
    public class GetLogEntriesQuery
    {
        #region Fields

        private readonly IAccessRightChecker accessRightChecker;
        private readonly IAuditLogReader auditLogReader;
        private readonly MapperConfiguration mapperConfiguration;

        #endregion Fields

        #region Constructor

        public GetLogEntriesQuery(IAccessRightChecker accessRightChecker, IAuditLogReader auditLogReader, MapperConfiguration mapperConfiguration)
        {
            this.accessRightChecker = accessRightChecker;
            this.auditLogReader = auditLogReader;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods
        
        public async Task<DataSourceResult> Execute(DataSourceRequest request)
        {
            //Права
            accessRightChecker.CheckIsAdmin();

            var result = await auditLogReader.GetAuditLogEntries()
                .Where(le => le.ParentEntryId == null)
                .OrderByDescending(le => le.DateAndTime)
                .ProjectTo<AuditLogEntryModel>(mapperConfiguration)
                .ToDataSourceResultAsync(request);

            return result;
        }

        #endregion Methods
    }
}
