using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace ECommerce.API.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class AddRequiredHeaderParameter : IOperationFilter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            //var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            //var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            // Check for authorize attribute
            //var hasAuthorize = context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().Any() ||
            //                   context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().Any();

            //var filterDescriptor = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            //var hasAuthorizedFilter = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            //var allowAnonymous = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            //var hasAuthorizedAttribute = context.MethodInfo.ReflectedType?.CustomAttributes.First().AttributeType ==
            //                             typeof(AuthorizeAttribute);
            //MethodInfo methodInfo;
            //var ss = context.ApiDescription.TryGetMethodInfo(out methodInfo);
            //var controllerFilters = methodInfo.DeclaringType.CustomAttributes;
            //var actionFilters = methodInfo.DeclaringType.CustomAttributes;
            //var hasAuthorize = controllerFilters.OfType<AuthorizeAttribute>().Any();

            var routeUrl = context.ApiDescription.ActionDescriptor.AttributeRouteInfo.Template;

            var found = false;
            if (context.ApiDescription.ActionDescriptor is
                Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controller)
            {
                found = controller.ControllerTypeInfo.GetCustomAttributes().OfType<AuthorizeAttribute>().Any();
            }

            if (found && routeUrl.Contains("/v2/"))
            {

                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "SirketGuid",
                    @In = ParameterLocation.Header,
                    Description = "Seçili şirkete ait guid değeri",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "uuid"
                    }
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "SubeGuid",
                    @In = ParameterLocation.Header,
                    Description = "Seçili şubeye ait guid değeri",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "uuid"
                    }
                });
            }
        }
    }
}
