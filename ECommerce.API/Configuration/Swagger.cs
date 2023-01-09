using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using ECommerce.API.Filter;

namespace ECommerce.API.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerOpen = Convert.ToBoolean(configuration["SwaggerOpen"] ?? "true");

            if (swaggerOpen)
            {
                // Register the Swagger generator, defining 1 or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.OperationFilter<SwaggerJsonIgnoreFilter>();

                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "ECommerce.API",
                        Description = " ASP.NET Core Web API",
                        TermsOfService = new Uri("https://soft.com.tr/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Soft Yazılım",
                            Email = string.Empty,
                            Url = new Uri("https://soft.com.tr"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://soft.com.tr/license"),
                        }
                    });



                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    //c.IncludeXmlComments(xmlPath);

                    
                    // using System.Reflection;
                     var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                    //c.OperationFilter<AddRequiredHeaderParameter>(); // headerdan parametre girmek için

                    c.OperationFilter<DosyaYuklemeOperationFilter>(); //  dosya yükleme açılması içindir
                });
                //services.AddSwaggerGenNewtonsoftSupport();
            }

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOpen = Convert.ToBoolean(configuration["SwaggerOpen"] ?? "true");
            if (!swaggerOpen) return app;
            app.UseSwagger(options =>
                {
                   options.SerializeAsV2 = true;
                });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Api V1");
                c.DocumentTitle = "ECommerce";
                //c.DisplayOperationId();
                c.DocExpansion(DocExpansion.List);
                //c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
