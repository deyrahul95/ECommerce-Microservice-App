using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace OrderApi.Application.Helpers;

public class AuthenticatedHttpClientHelper(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(token) is false && token.StartsWith("Bearer "))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token["Bearer ".Length..]);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}