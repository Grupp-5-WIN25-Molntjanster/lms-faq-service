using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace FaqService.Api.OpenApi;

public class FaqDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
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
