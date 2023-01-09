using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using ECommerce.Core.Aspects.Autofac.Exception;
using ECommerce.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace ECommerce.Core.Utilities.Interceptors
{
   public class AspectInterceptorSelector:IInterceptorSelector
    {
        private IInterceptorSelector _interceptorSelectorImplementation;

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes =
                (type.GetMethod(method.Name) ??
                 throw new InvalidOperationException("AspectInterceptorSelector method.Name null geldi."))
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));

            return classAttributes.OrderBy(o => o.Priority).ToArray();
        }
    }
}
