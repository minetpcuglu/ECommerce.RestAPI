using Microsoft.Extensions.DependencyInjection;
using ECommerce.Business.Security;
using ECommerce.Core.Utilities.Security.Jwt;
using ECommerce.Data.AUDIT;
using ECommerce.Data.EF.Security;
using ECommerce.Data.EF;

namespace ECommerce.API.Configuration
{
    public static class Service
    {
        /// <summary>
        /// When the program runs, services are injected.
        /// </summary>
        /// <param name="services"></param>
        public static void AddMyServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

            services.AddScoped<IRoleAuthorizationService, RoleAuthorizationService>();
            services.AddScoped<IRoleAuthorizationRepository, RoleAuthorizationRepository>();

            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            services.AddScoped<IAuditRepository, AuditRepository>();



        }
    }
}
