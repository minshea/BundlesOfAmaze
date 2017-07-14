using Autofac;
using BundlesOfAmaze.Application;
using BundlesOfAmaze.Data;

namespace BundlesOfAmaze.InversionOfControl
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<IDataContext>().InstancePerLifetimeScope();

            builder.RegisterType<CatRepository>().As<ICatRepository>();
            builder.RegisterType<CommandService>().As<ICommandService>();
        }
    }
}