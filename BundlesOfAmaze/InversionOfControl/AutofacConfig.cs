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

            builder.RegisterType<CatRepository>().As<ICatRepository>();
            builder.RegisterType<OwnerRepository>().As<IOwnerRepository>();

            builder.RegisterType<CommandService>().As<ICommandService>();
            builder.RegisterType<CreateCommandService>().As<ICreateCommandService>();
            builder.RegisterType<HelpCommandService>().As<IHelpCommandService>();
            builder.RegisterType<GiveCommandService>().As<IGiveCommandService>();
            builder.RegisterType<ListCommandService>().As<IListCommandService>();
        }
    }
}