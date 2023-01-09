using ECommerce.Domain.Infrastructure;
using ECommerce.Domain.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Configuration
{
    public static class DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddMyDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PostgreSQL");
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString, builder =>
            {
                //builder.EnableRetryOnFailure();
                // builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(180), null);
                builder.CommandTimeout(180); // 3 minutes
                                             // builder.ExecutionStrategy()
            }), ServiceLifetime.Transient);

        }
    }
}
