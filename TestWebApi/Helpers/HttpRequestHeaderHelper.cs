using System.Text;

namespace TestWebApi.Helpers;

/// <summary>
/// Header 정보 가져와서 처리하는 로직들
/// </summary>
public abstract class HttpRequestHeaderHelper
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="header"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static (bool isSuccess, string headerValue) HeaderValidationAndGetHeader(
        HeaderType header,
        HttpRequest request
    )
    {
        // No authorization header, so throw no result.
        if (!request.Headers.ContainsKey("Authorization"))
            return (false, string.Empty);

        var authorizationHeader = request.Headers["Authorization"].ToString();

        // If authorization header doesn't start with basic, throw no result.
        if (!authorizationHeader.StartsWith(header + " ", StringComparison.OrdinalIgnoreCase))
            return (false, string.Empty);

        return (true, authorizationHeader.Replace(header.ToString(), "").Trim());
    }

    public static string[] AuthSplit(HttpRequest request)
    {
        var authorizationHeader = request.Headers["Authorization"].ToString();

        var authBase64Decoded = Encoding.UTF8.GetString(
            Convert.FromBase64String(
                authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)
            )
        );
        var authSplit = authBase64Decoded.Split(new[] { ':' }, 2);
        return authSplit;
    }
}