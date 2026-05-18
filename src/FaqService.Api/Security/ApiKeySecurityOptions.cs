namespace FaqService.Api.Security;

public sealed class ApiKeySecurityOptions
{
    public const string SectionName = "ApiKey";

    public string HeaderName { get; init; } = "X-API-KEY";

    public string Value { get; init; } = null!;
}
