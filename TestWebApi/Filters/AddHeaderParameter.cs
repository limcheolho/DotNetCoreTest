using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestWebApi.Filters;

public class AddHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // operation.Parameters ??= [];

        operation.Parameters.Add(
            new OpenApiParameter
            {
                Name = "X-Authorization",
                In = ParameterLocation.Header,
                Required = false,
                Description = " (login과 refresh에서만 사용. 다른곳에선 무시하세요.)",
                Schema = new OpenApiSchema { Type = "String" },
            }
        );
    }
}