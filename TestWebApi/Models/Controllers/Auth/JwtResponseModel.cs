namespace TestWebApi.Models;

public class JwtResponseModel
{
    public JwtResponseModel() { }

    public JwtResponseModel(string accessToken, string refreshToken)
    {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
    }

    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}
