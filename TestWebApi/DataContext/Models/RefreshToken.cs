namespace TestWebApi.DataContext.Models;

public class RefreshToken
{
    public string refreshToken { get; set; }

    public string userId { get; set; }
    public DateTime refreshTokenExpiryTime { get; set; }
    public User? user { get; set; }
}