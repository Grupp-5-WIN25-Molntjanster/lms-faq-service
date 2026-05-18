using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace FaqService.Api.Security;

public sealed class ApiKeyAuthFilter(IOptions<ApiKeySecurityOptions> options) : IAsyncActionFilter
{
    private readonly ApiKeySecurityOptions _options = options.Value;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (string.IsNullOrWhiteSpace(_options.Value))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(_options.HeaderName, out var providedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!string.Equals(providedApiKey, _options.Value, StringComparison.Ordinal))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}
