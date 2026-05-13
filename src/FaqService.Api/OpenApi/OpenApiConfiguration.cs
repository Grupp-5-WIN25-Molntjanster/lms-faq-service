namespace FaqService.Api.OpenApi;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddFaqOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<FaqDocumentTransformer>();
            options.AddSchemaTransformer<FaqSchemaTransformer>();
        });

        return services;
    }
}
