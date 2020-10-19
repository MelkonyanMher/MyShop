using System.Reflection;
using Autofac;
using AutoMapper;
using Tionit.Enterprise;
using Tionit.ShopOnline.Application;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Backoffice.Application.Commands;
using Module = Autofac.Module;

namespace Tionit.ShopOnline.Backoffice.AutofacModule
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //application
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AccessRightChecker)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AccessRightChecker)))
                .InstancePerLifetimeScope();

            //Application (Backoffice)
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AuthenticateAdministratorCommand)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AuthenticateAdministratorCommand)))
                .InstancePerLifetimeScope();

            // UserInfoProvider
            builder
                .RegisterType<UserInfoProvider>()
                .As<IUserInfoProvider>()
                .As<IAppUserInfoProvider>()
                .As<IAppUserInfoSetter>()
                .InstancePerLifetimeScope();

            // Automapper
            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                    cfg.AddMaps(typeof(AccessRightChecker).Assembly,    //Application
                        typeof(AuthenticateAdministratorCommand).Assembly, //application.Backoffice
                        typeof(Startup).Assembly); //Backoffice
                });
            builder.Register(context => mapperConfiguration);
        }
    }
}
