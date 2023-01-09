using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.API.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class Cors
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyCors(this IServiceCollection services, IConfiguration configuration)
        {

            string[] origins = { };
            var corsOriginSiteler = configuration["Cors:IzinVerilenSiteler"];
            if (!string.IsNullOrEmpty(corsOriginSiteler))
            {
                origins = corsOriginSiteler.Split(",", StringSplitOptions.RemoveEmptyEntries);
            }

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAcik",

                    builder =>
                    {
                        builder
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(5000))
                            .SetIsOriginAllowed(origin => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
                options.AddPolicy("CorsOzel",
                    builder =>
                    {
                        builder
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(5000))
                            .WithOrigins(origins)
                            .WithHeaders(new[] { "authorization", "content-type", "accept" })
                            .WithMethods(new[] { "GET", "POST", "PUT","PATCH", "DELETE", "OPTIONS" })
                            .AllowAnyHeader()
                          //  .AllowAnyMethod()
                            .AllowCredentials()
                            .Build();
                    }
                );
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMyCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            //cors ayarlarını okuyalım
            var herIstegeAcik = Convert.ToBoolean(configuration["Cors:HerIstegeAcik"] ?? "true");
            app.UseCors(herIstegeAcik ? "CorsAcik" : "CorsOzel");
            return app;
        }
    }
}
