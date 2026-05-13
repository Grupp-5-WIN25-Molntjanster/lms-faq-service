using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using FaqService.Application.DTOs;
using System.Text.Json.Nodes;


namespace FaqService.Api.OpenApi;


public class FaqSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = context.JsonTypeInfo.Type;

        if (type == typeof(FaqResult))
        {
            schema.Example = new JsonObject
            {
                ["id"] = 1,
                ["title"] = "What is the return policy?",
                ["summary"] = "You can return any item.",
                ["content"] = "You can return any item within 30 days of purchase. If you have any questions, please contact our support.",
                ["displayOrder"] = 1
            };
        }
        else if (type == typeof(FaqRequest))
        {
            schema.Example = new JsonObject
            {
                ["title"] = "What is the return policy?",
                ["summary"] = "You can return any item.",
                ["content"] = "You can return any item within 30 days of purchase. If you have any questions, please contact our support."
            };
        }
        return Task.CompletedTask;
    }
}
