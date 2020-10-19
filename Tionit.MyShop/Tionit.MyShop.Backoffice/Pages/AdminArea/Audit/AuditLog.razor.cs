using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using Tionit.Enterprise.Exceptions;
using Tionit.ShopOnline.Backoffice.Application.Queries.Log;
using Tionit.ShopOnline.Backoffice.Application.Queries.Log.Models;
using Tionit.ShopOnline.Backoffice.InteropServices;

namespace Tionit.ShopOnline.Backoffice.Pages.AdminArea.Audit
{
    public partial class AuditLog
    {
        #region Inject

        [Inject]
        public Executor<GetLogEntriesQuery> GetLogEntryesQueryExecutor { get; set; }

        [Inject]
        public Messages Messages { get; set; }

        #endregion Inject

        #region Fields

        /// <summary>
        /// Запись в лог, отображаемые в данный момент в гриде
        /// </summary>
        private IEnumerable<AuditLogEntryModel> LogEntries { get; set; }

        /// <summary>
        /// Общее количество логов
        /// </summary>
        private int TotalCount { get; set; }

        #endregion Fields

        #region Methods

        protected async Task OnGridNeedItems(GridReadEventArgs args)
        {
            try
            {
                var result = await GetLogEntryesQueryExecutor.Execute(query => query.Execute(args.Request));
                LogEntries = result.Data.Cast<AuditLogEntryModel>();
                TotalCount = result.Total;
            }
            catch (BusinessException businessException)
            {
                await Messages.ShowError(businessException.Message);
            }
        }

        #endregion Methods
    }
}
