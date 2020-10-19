using Autofac;
using Tionit.ShopOnline.Persistence;
using Tionit.Persistence;
using Tionit.ShopOnline.Infrastructure.MessageFormers;
using Tionit.ShopOnline.Application.Contract.Infrastructure.MessageFormer;

namespace Tionit.ShopOnline.Portal.ApplicationModule
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

            #region Infrastructure.MessageFormers

            builder
                .RegisterType<OrderCreationNotificationMessageFormer>()
                .As<IOrderCreationNotificationMessageFormer>()
                .InstancePerLifetimeScope();

            #endregion Infrastructure.MessageFormers
        }
    }
}
