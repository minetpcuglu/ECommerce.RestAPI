using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using ECommerce.Business.Audit;
//using ECommerce.Business.Security;
using ECommerce.Core.Utilities.Interceptors;
using ECommerce.Core.Utilities.Security.Jwt;
using ECommerce.Data.AUDIT;
using ECommerce.Data.EF;
using ECommerce.Data.EF.Security;
using ECommerce.Domain.Infrastructure;
using ECommerce.Domain.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>().AsSelf();
            builder.RegisterType<DbContextOptions<DatabaseContext>>().AsSelf();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
     
            //builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();

            //builder.RegisterType<AuthorizationService>().As<IAuthorizationService>();
            builder.RegisterType<AuthorizationRepository>().As<IAuthorizationRepository>();

           // builder.RegisterType<RoleAuthorizationService>().As<IRoleAuthorizationService>();
            builder.RegisterType<RoleAuthorizationRepository>().As<IRoleAuthorizationRepository>();

            //builder.RegisterType<UserRoleService>().As<IUserRoleService>();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>();

            builder.RegisterType<AuditRepository>().As<IAuditRepository>();
            builder.RegisterType<AuditService>().As<IAuditService>();

  
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();



        }
    }
}
