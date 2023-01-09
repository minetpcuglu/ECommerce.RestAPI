using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
