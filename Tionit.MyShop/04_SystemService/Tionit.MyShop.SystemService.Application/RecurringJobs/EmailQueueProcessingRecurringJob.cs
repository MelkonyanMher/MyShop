﻿using Hangfire;
using System.Threading.Tasks;
using Tionit.Enterprise.HangfireRecurringJobs;
using Tionit.Net.Email;
using Tionit.ShopOnline.Application.Contract.RecurringJobs;

namespace Tionit.ShopOnline.SystemService.Application.RecurringJobs
{
    public class EmailQueueProcessingRecurringJob :HangfireRecurringJob, IEmailQueueProcessingRecurringJob
    {
        #region Fields

        private readonly IEmailQueueProcessor emailQueueProcessor;

        #endregion Fields

        #region Constructor

        public EmailQueueProcessingRecurringJob(IEmailQueueProcessor emailQueueProcessor)
        {
            this.emailQueueProcessor = emailQueueProcessor;
        }

        #endregion Constructor

        #region Methods

        #region PRoperties

        public override string CronExpression => Cron.Minutely();

        #endregion Properties

        public override async Task ExecuteAsync(IJobCancellationToken jobCancellationToken)
        {
            await emailQueueProcessor.ProcessQueueAsync(jobCancellationToken.ShutdownToken);
        }

        #endregion Methods
    }
}
