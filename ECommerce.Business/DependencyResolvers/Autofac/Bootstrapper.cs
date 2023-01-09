using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Business.DependencyResolvers.Autofac
{
    public static class Bootstrapper
    {
        public static IContainer Container { get; private set; }
        public static AutofacServiceProvider InitializeContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacBusinessModule());
           
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }
    }
}
