using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Core.CrossCuttingConcerns.Caching;
using ECommerce.Core.CrossCuttingConcerns.Caching.Microsoft;
using ECommerce.Core.Utilities.IoC;

namespace ECommerce.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        { 
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();

        }
    }
}
