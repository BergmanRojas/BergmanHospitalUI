using System.Net.Http.Headers;

namespace BergmanHospitalUI.Services;

public class CustomAuthorizationMessageHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public CustomAuthorizationMessageHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _tokenProvider.Token;

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
