using System;
using Castle.DynamicProxy;

namespace ECommerce.Core.Utilities.Interceptors
{
    //Interceptor'lar belirli noktalarda metot çağrımları sırasında araya girerek bizlerin çakışan ilgilerimizi işletmemizi ve yönetmemizi sağlamakta. Böylece metotların çalışmasından önce veya sonra bir takım işlemleri gerçekleştirebilmekteyiz
   public abstract class MethodInterception: MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception exception) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation,e);
                throw e;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
