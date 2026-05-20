using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace FaqService.Api.OpenApi;

public class FaqDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["ApiKey"] = new OpenApiSecurityScheme {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-API-KEY",
                Description = "API key is required to access the API. Please provide a valid API key in the 'X-API-KEY' header."
            }
        };

        foreach (var path in document.Paths.Values)
        {
            if (path.Operations is null) continue;

            foreach (var operation in path.Operations)
            {
                operation.Value.Security ??= [];

                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("ApiKey", document)] = []
                });

            }
        }

        document.Info = new OpenApiInfo()
        {
            Title = "FAQ API",
            Version = "v1",
            Description = """
            An API for managing FAQ articles,
            allowing clients to perform CRUD operations on FAQs.
            """,

            Contact = new OpenApiContact()
            {
                Name = "FAQ API Support",
                Url = new Uri("https://github.com/alnils"),
            }
        };

        return Task.CompletedTask;
    }
}
