﻿using System;
using Castle.DynamicProxy;

namespace ECommerce.Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true,Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute: Attribute, IInterceptor
    {
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}