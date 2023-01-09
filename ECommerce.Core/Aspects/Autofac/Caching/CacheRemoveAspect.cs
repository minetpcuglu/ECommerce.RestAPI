using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Core.CrossCuttingConcerns.Caching;
using ECommerce.Core.Utilities.Interceptors;
using ECommerce.Core.Utilities.IoC;

namespace ECommerce.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
