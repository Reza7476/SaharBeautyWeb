using System.Net.Http.Headers;

namespace SaharBeautyWeb.Configurations.DelegateHandler;

public class AuthTokenHandler: DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthTokenHandler(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");

        if(!string.IsNullOrEmpty(token)) 
        {
            request.Headers.Authorization=new AuthenticationHeaderValue("Bearer",token);
        }

        return base.SendAsync(request, cancellationToken);
    }

}
