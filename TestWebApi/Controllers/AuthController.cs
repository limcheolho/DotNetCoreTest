using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models;
using TestWebApi.Services.AuthService;

namespace TestWebApi.Controllers;

/// <summary>
///     인증
/// </summary>
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly JwtHelper _jwtHelper;

    /// <summary>
    ///     인증
    /// </summary>
    public AuthController(IAuthService authService, JwtHelper jwtHelper)
    {
        _authService = authService;
        _jwtHelper = jwtHelper;
    }


    /// <summary>
    ///     아이디 찾기
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Route("id")]
    [HttpGet]
    public async Task<IActionResult> FindId(string userId)
    {
        var userFromDb = await _authService.FindOneAsync(userId: userId);
        return Ok(
            userFromDb == null
                ? new ReponseModel<string>(isSuccess: true)
                : new ReponseModel<string>(true, "이미 존재하는 아이디입니다.")
        );
    }
    
    
    /// <summary>
    ///     사용자정보 가져오기
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userNm"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("users")]
    public async Task<IActionResult> GetUsers(string userId, string userNm)
    {
        var data = await _authService.FindUsersAsync(userId, userNm);
        data.ToList().ForEach(g => g.password = string.Empty);
        return Ok(data);
    }
    
    
    /// <summary>
    ///     사용자 비밀번호 업데이트
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("password")]
    public async Task<IActionResult> UpdatePassword([FromBody] User user)
    {
        var checkPasswordAndEmail = LoginExtensions.CheckPasswordAndEmail(
            user.email,
            user.password!,
            user.passwordAgain!
        );
        if (!string.IsNullOrEmpty(checkPasswordAndEmail))
            return Ok(new ReponseModel<string>(false, checkPasswordAndEmail));
        var hasher = new PasswordHasher<string>();
        var hashedPassword = hasher.HashPassword(null, user.password);
        user.password = hashedPassword;
        await _authService.UpdateUserPasswordAsync(user);
        return Ok(new ReponseModel<string>(true, string.Empty));
    }

}