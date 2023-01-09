  
using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace ECommerce.API.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class DosyaYuklemeOperationFilter : IOperationFilter
    {
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation?.OperationId != null && operation.OperationId.ToLower().Contains("dosyayukleme"))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Description = "file to upload",
                    Content = new Dictionary<String, OpenApiMediaType>
                    {
                        {
                            "multipart/form-data", new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Required = new HashSet<String> {"dosya"},
                                    Properties = new Dictionary<String, OpenApiSchema>
                                    {
                                        {
                                            "dosya", new OpenApiSchema()
                                            {
                                                // matches our handcrafted yaml
                                                Type = "string",
                                                Format = "binary"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }

    }
}
