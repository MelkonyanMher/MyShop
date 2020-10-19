using Autofac;
using Tionit.ShopOnline.Persistence;
using Tionit.Persistence;

namespace Tionit.ShopOnline.Backoffice.AutofacModule
{
    /// <summary>
    /// Модуль для регистрации компанентов из Persistence
    /// </summary>
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ChangesSaver<AppDbContext>>()
                .As<IChangesSaver>()
                .InstancePerLifetimeScope();
        }
    }
}
