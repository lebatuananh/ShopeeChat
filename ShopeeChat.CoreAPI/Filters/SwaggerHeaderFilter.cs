using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ShopeeChat.CoreAPI.Filters
{
    public class SwaggerHeaderFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-AutoPortal-App-Token",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = true // set to false if this is optional
            });
        }
    }
}