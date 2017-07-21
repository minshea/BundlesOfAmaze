using Autofac;
using BundlesOfAmaze.Application;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.InversionOfControl
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<DataContext>().As<IDataContext>().WithParameter("connectionString", connectionString).InstancePerLifetimeScope();
            builder.RegisterType<BackgroundService>().As<IBackgroundService>();
            builder.RegisterType<OwnerService>().As<IOwnerService, ICurrentOwner>().InstancePerLifetimeScope();

            builder.RegisterType<CatRepository>().As<ICatRepository>();
            builder.RegisterType<OwnerRepository>().As<IOwnerRepository>();
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
            builder.RegisterType<AdventureEntryRepository>().As<IAdventureEntryRepository>();
            builder.RegisterType<AdventureRepository>().As<IAdventureRepository>();
        }
    }
}