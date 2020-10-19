using System;
using System.Linq;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Telerik.Blazor.Services;
using Tionit.AuditLogging;
using Tionit.AuditLogging.Repositories.EntityCoreAuditLogRepository;
using Tionit.Enterprise;
using Tionit.Net.Email;
using Tionit.Net.Email.EFCoreEmailQueueRepository;
using Tionit.ShopOnline.Application.AuthOptions;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Backoffice.Application.Commands;
using Tionit.ShopOnline.Backoffice.InteropServices;
using Tionit.ShopOnline.Domain;
using Tionit.ShopOnline.Persistence;

namespace Tionit.ShopOnline.Backoffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Контейнер Autofac
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region Localization

            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
            });

            #endregion

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                if (Environment.IsDevelopment())
                {
                    o.DetailedErrors = true;
                }
            });
            services.AddTelerikBlazor();

            services.AddSingleton<ITelerikStringLocalizer>(new Localizer());

            services.AddBlazoredLocalStorage();

            // InteropServices
            services.AddInteropServices();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidIssuer = AdminAuthOptions.Issuer, // строка, представляющая издателя

                        ValidateAudience = true, // будет ли валидироваться потребитель токена
                        ValidAudience = AdminAuthOptions.Audience,// установка потребителя токена

                        ValidateLifetime = false,  // будет ли валидироваться время существования

                        IssuerSigningKey = AdminAuthOptions.GetSymmetricSecurityKey(), // установка ключа безопасности
                        ValidateIssuerSigningKey = true // валидация ключа безопасности
                    };
                });

            services.AddOpenApiDocument(document =>
            {
                document.GenerateEnumMappingDescription = true;
                document.DocumentName = "online shop Api";
                document.Version = "v1";
                document.Title = "Tionit.OnlineShop";
                document.GenerateExamples = true;
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("jwt-auth"));
                document.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("jwt-auth", new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header
                    }));
                document.GenerateXmlObjects = false;
            });

            //EF
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            #region Регистрация Tionit-овских  компонентов

            services.AddScoped<IDateTimeProvider, DefaultDateTimeProvider>();

            //email enqueuer
            services.AddScoped<IEmailQueueRepository>(provider => new EFCoreEmailQueueRepository(connectionString));
            services.AddScoped<IEmailEnqueuer, EmailEnqueuer>();

            //// глобальные переменные
            //services.AddGlobalSystemVariablesWithSqlServerStorage<SystemVariableType>(connectionString);
            //services.AddScoped<IGlobalSystemVariablesProvider, GlobalSystemVariablesProvider>();

            // Hangfire
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString));

            //лог
            services.AddScoped<IAuditLogRepository>(c => new EntityAuditLogRepository(connectionString));
            services.AddScoped<IAuditLogger>(c => AuditLogger
                .StartInitialization(c.GetService<IAuditLogRepository>(), "OSH")
                .UserInfoProvider(c.GetService<IUserInfoProvider>())
                .Subsystem("OnlineShop bacoffice")
                .Initialize());
            services.AddScoped<IAuditLogReader, AuditLogReader>();
            services.AddScoped<IAuditLogCleaner, AuditLogCleaner>();

            #endregion Регистрация Tionit-овских  компонентов

            ContainerBuilder containerBuilder = new ContainerBuilder();

            //регистрируем все DI модули данного проекта
            containerBuilder.RegisterAssemblyModules(typeof(Startup).Assembly);

            containerBuilder.RegisterType<UserSession>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterGeneric(typeof(Executor<>))
                .InstancePerLifetimeScope();

            containerBuilder.Populate(services);
            ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime, IWebHostEnvironment env)
        {
            try
            {
                #region Localization

                app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

                #endregion

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                // включаем Hangfire Dashboard
                app.UseHangfireDashboard("/admin/audit/background-tasks", new DashboardOptions
                {
                    Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
                });

                app.UseRouting();

                // настраиваем Jwt
                app.UseAuthentication();
                app.UseAuthorization();

                // Register the Swagger generator and the Swagger UI middlewares
                app.UseOpenApi();
                app.UseSwaggerUi3();

                // установка информации в UserInfoProvider
                app.Use((context, next) =>
                {
                    IAppUserInfoSetter userInfoProvider =
                        context.RequestServices.GetService<IAppUserInfoSetter>();

                    var userName = context.User.Identity.Name;
                    string idFromTokenStr = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userName != null)
                    {
                        userInfoProvider.UserName = userName;
                        if (idFromTokenStr != null)
                            userInfoProvider.UserTokenId = Guid.Parse(idFromTokenStr);

                        userInfoProvider.UserType = UserType.Authenticated;
                        if (context.User.IsInRole(UserRole.Admin.ToString()))
                            userInfoProvider.UserRole = UserRole.Admin;
                    }
                    else
                        userInfoProvider.UserType = UserType.Anonymous;

                    return next();
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapControllers();
                    endpoints.MapFallbackToPage("/_Host");
                });

                //// включаем Hangfire Dashboard
                //app.UseHangfireDashboard("admin/audit/background-tasks", new DashboardOptions
                //{
                //    Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
                //});

                using (var lifetimeScope = app.ApplicationServices.CreateScope())
                {
                    // текущий пользователь - система
                    IAppUserInfoSetter appUserInfoProvider =
                        lifetimeScope.ServiceProvider.GetRequiredService<IAppUserInfoSetter>();
                    appUserInfoProvider.UserType = UserType.System;

                    CreateDefaultAdminCommand createDefaultAdminCommand =
                        lifetimeScope.ServiceProvider.GetRequiredService<CreateDefaultAdminCommand>();
                    createDefaultAdminCommand.Execute().Wait();
                }

                // диспоузим контейнер автофака при остановке приложения
                appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
            }
            catch (Exception exception)
            {
                using ILifetimeScope lifetimeScope = ApplicationContainer.BeginLifetimeScope();
                IAuditLogger auditLogger = lifetimeScope.Resolve<IAuditLogger>();
                auditLogger.Error("Ошибка при старте API")
                    .Exception(exception)
                    .Log();
                throw;
            }
        }
    }
}
