using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tionit.Enterprise;
using Tionit.ShopOnline.Application;
using Tionit.ShopOnline.Application.Contract;
using Tionit.ShopOnline.Portal.Application.Command.Customers;
using Module = Autofac.Module;

namespace Tionit.ShopOnline.Portal.ApplicationModule
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

            //Application (Portal)
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AuthenticateCustomerCommand)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder
                .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AuthenticateCustomerCommand)))
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
                        typeof(AuthenticateCustomerCommand).Assembly, //application.Portal
                        typeof(Startup).Assembly); //Portal
                });
            builder.Register(context => mapperConfiguration);
        }
    }
}
