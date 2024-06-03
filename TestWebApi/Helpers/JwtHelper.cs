using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TestWebApi.Helpers;

public class JwtHelper
{
    private readonly IOptions<JwtSettings> _jwtSettings;

    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }


    /// <summary>
    /// 토큰 발급
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public (string accessToken, string refreshToken) GetJwtSecurityToken(
        string userId
    )
    {
        var authClaims = new List<Claim>
        {
            new("userId", userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var accessToken = CreateToken(authClaims: authClaims);
        var refreshToken = GenerateRefreshToken();
        
        //accessToken 암호화
        var newAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);

        return (newAccessToken, refreshToken);
    }


    /// <summary>
    ///  리프레쉬 토큰 생성
    /// </summary>
    /// <returns></returns>
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }


    /// <summary>
    ///     토큰 생성
    /// </summary>
    /// <param name="authClaims"></param>
    /// <returns></returns>
    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret!)
        );

        var token = new JwtSecurityToken(
            _jwtSettings.Value.ValidIssuer,
            _jwtSettings.Value.ValidAudience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.Value.TokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                authSigningKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return token;
    }
}