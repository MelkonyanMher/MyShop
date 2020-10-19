using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Tionit.AuditLogging;
using Tionit.AuditLogging.Repositories.EntityCoreAuditLogRepository;
using Tionit.Enterprise;
using Tionit.Enterprise.HangfireRecurringJobs;
using Tionit.Net.Email;
using Tionit.Net.Email.EFCoreEmailQueueRepository;
using Tionit.Persistence;
using Tionit.ShopOnline.Application;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Persistence;
using Tionit.ShopOnline.SystemService.Application.Administrators;
using Tionit.ShopOnline.SystemService.Application.RecurringJobs;

namespace Tionit.ShopOnline.SystemService
{
    public class Startup
    {
        #region Constructor

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Текущая среда
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Контейнер Autofac
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        #endregion Property

        #region Methods

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Default");

            //EF
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            //DateTimeProvider
            services.AddScoped<IDateTimeProvider, DefaultDateTimeProvider>();

            //email procesing
            services.AddScoped<IEmailQueueRepository>(provider =>
                new EFCoreEmailQueueRepository(connectionString));
            string systemName = Configuration["App:SystemName"];
            string systemEmail = Configuration["App:SystemEmail"];
            SmtpSettings smtpSettings = new SmtpSettings
            {
                SmtpHost = Configuration["SmtpSettings:Host"],
                SmtpPort = Configuration.GetValue<int>("SmtpSettings:Port"),
                SmtpUsername = Configuration["SmtpSettings:UserName"],
                SmtpPassword = Configuration["SmtpSettings:Password"],
                SmtpEnableSsl = Configuration.GetValue<bool>("SmtpSettings:EnableSsl")
            };

            //лог
            services.AddScoped<IAuditLogRepository>(c => new EntityAuditLogRepository(connectionString));
            services.AddScoped<IAuditLogger>(c => AuditLogger
                    .StartInitialization(c.GetService<IAuditLogRepository>(), "ShopOnline")
                    .UserInfoProvider(c.GetService<IUserInfoProvider>())
                    .EmailConfiguration(c.GetService<IEmailEnqueuer>(), systemEmail)
                    .Subsystem("Системная служба")
                    .Initialize());

            services.AddEmailQueueProcessing(systemEmail, systemName, smtpSettings);

            //  health check
            //services.AddHealthChecks()
            //    .AddDbContextCheck<AppDbContext>()
            //    .AddPrivateMemoryHealthCheck(1000_000_000L)
            //    .AddWorkingSetHealthCheck(1000_000_000L)
            //    .AddDiskStorageHealthCheck(x => x.AddDrive(Configuration["HealthCheck:Drive"], 10240L))
            //    .AddUrlGroup(new Uri(Configuration["HealthCheck:AdminUrl"]), "Админка ядра")
            //    .AddUrlGroup(new Uri(Configuration["HealthCheck:ApiUrl"]), "API ядра")
            //    .AddHangfire(options =>
            //    {
            //        options.MinimumAvailableServers = 1;
            //        options.MaximumJobsFailed = 30;
            //    });

            // Hangfire
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString));

            // периодические задачи

            services.AddRecurringJobs(typeof(EmailQueueProcessingRecurringJob).Assembly);

            ContainerBuilder containerBuilder = new ContainerBuilder();

            // Application
            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AccessRightChecker)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AccessRightChecker)))
                .InstancePerLifetimeScope();

            //SystemService.Application
            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CreateDefaultAdministratorCommand)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CreateDefaultAdministratorCommand)))
                .InstancePerLifetimeScope();

            // репозитории
            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType<ChangesSaver<AppDbContext>>()
                .As<IChangesSaver>()
                .InstancePerLifetimeScope();

            // UserInfoProvider
            UserInfoProvider userInfoProvider = new UserInfoProvider
            {
                UserName = "Система",
                UserType = UserType.System
            };

            containerBuilder
                .Register(context => userInfoProvider)
                .As<IUserInfoProvider>()
                .As<IAppUserInfoProvider>()
                .As<IAppUserInfoSetter>()
                .InstancePerLifetimeScope();

            containerBuilder.Populate(services);
            ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime, IWebHostEnvironment env)
        {
            try
            {
                // мигрируем БД
                using (var lifetimeScope = ApplicationContainer.BeginLifetimeScope())
                {
                    using var dbContext = lifetimeScope.Resolve<AppDbContext>();
                    dbContext.Database.Migrate();
                }

                // haltcheck
                //app.UseHealthChecks("/healthz", new HealthCheckOptions
                //{
                //    Predicate = registration => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});

                app.UseHangfireServer();

                // периодические задачи
                app.UseRecurringJobs();

                // выполняем стартовые задачи
                using (var lifetimeScope = app.ApplicationServices.CreateScope())
                {
                    // текущий пользователь система
                    IAppUserInfoSetter appUserInfoProvider = lifetimeScope.ServiceProvider.GetRequiredService<IAppUserInfoSetter>();
                    appUserInfoProvider.UserType = UserType.System;

                    //SystemDataInitializer systemDataInitializer =
                    //    lifetimeScope.ServiceProvider.GetRequiredService<SystemDataInitializer>();
                    //systemDataInitializer.InitData().Wait();

                    //пишем в лог о запуске системной службы
                    IAuditLogger logger = lifetimeScope.ServiceProvider.GetRequiredService<IAuditLogger>();
                    logger.Info("Системная служба запущена").Log();
                }

                appLifetime.ApplicationStopped.Register(()=>
                {
                    //пишем в лог об остановке службы
                    using (var lifetimeScope = ApplicationContainer.BeginLifetimeScope())
                    {
                        // текущий пользователь - система
                        IAppUserInfoSetter appUserInfoProvider = lifetimeScope.Resolve<IAppUserInfoSetter>();
                        appUserInfoProvider.UserType = UserType.System;

                        IAuditLogger logger = lifetimeScope.Resolve<IAuditLogger>();
                        logger.Info("Системная служба остановлена").Log();
                    }

                    ApplicationContainer.Dispose();
                });
            }
            catch(Exception exception)
            {
                using ILifetimeScope lifetimeScope = ApplicationContainer.BeginLifetimeScope();
                IAuditLogger auditLogger = lifetimeScope.Resolve<IAuditLogger>();
                auditLogger.Error("Ошибка при старте системной службы")
                    .Exception(exception)
                    .Log();
                throw;
            }
        }

        #endregion Methods
    }
}
