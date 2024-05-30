#nullable enable
namespace TestWebApi.Helpers;

public class HttpContextExtensions
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextExtensions(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public HttpRequest? GetHttpRequest()
    {
        return _httpContextAccessor.HttpContext?.Request;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public String? GetIp()
    {
        if (_httpContextAccessor.HttpContext == null)
            return "batch";

        var ipAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        return ipAddress;
    }
}