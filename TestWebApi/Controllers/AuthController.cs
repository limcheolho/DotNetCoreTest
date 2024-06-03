using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models;
using TestWebApi.Services.AuthService;

namespace TestWebApi.Controllers;

/// <summary>
///     인증
/// </summary>
[Route("auth")]
[ApiController]
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
    [Route("id/{userId}")]
    [HttpGet]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(ReponseModel<string>), 200)]
    public async Task<IActionResult> FindId(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return StatusCode(400, "아이디를 입력하세요.");
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
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(List<User>), 200)]
    public async Task<IActionResult> GetUsers(string? userId, string? userNm)
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
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdatePassword([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.password))
            return StatusCode(400, "비밀번호가 입력되지 않았습니다.");

        var checkPassword = LoginExtensions.CheckPassword(user.password!, user.passwordAgain!);
        if (!string.IsNullOrEmpty(checkPassword))
            return BadRequest(checkPassword);
        var hasher = new PasswordHasher<string>();
        var hashedPassword = hasher.HashPassword(null, user.password);
        user.password = hashedPassword;
        await _authService.UpdateUserPasswordAsync(user);
        return Ok();
    }

    /// <summary>
    ///     사용자 정보 업데이트
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("edit")]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if (!string.IsNullOrEmpty(user.email))
        {
            var checkEmail = LoginExtensions.CheckEmail(user.email!);

            if (!string.IsNullOrEmpty(checkEmail))
                return BadRequest(checkEmail);
        }

        await _authService.UpdateUserAsync(user);
        return Ok();
    }

    /// <summary>
    ///     회원가입
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Route("join")]
    [HttpPost]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Join([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.userId))
            return BadRequest("아이디가 입력되지 않았습니다.");
        if (user.userId.Length < 5)
            return BadRequest("아이디는 5자리 이상이어야 합니다.");

        if (!string.IsNullOrEmpty(user.email))
        {
            var checkEmail = LoginExtensions.CheckEmail(user.email!);

            if (!string.IsNullOrEmpty(checkEmail))
                return BadRequest(checkEmail);
        }

        if (string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.password))
        {
            return StatusCode(400, "비밀번호가 입력되지 않았습니다.");
        }

        var checkPassword = LoginExtensions.CheckPassword(user.password!, user.passwordAgain!);
        if (!string.IsNullOrEmpty(checkPassword))
            return BadRequest(checkPassword);

        var userFromDb = await _authService.FindOneAsync(userId: user.userId);
        if (userFromDb != null)
            return BadRequest("이미 사용중인 아이디입니다.");

        var hasher = new PasswordHasher<string>();
        var hashedPassword = hasher.HashPassword(null, user.password);

        await _authService.CreateUserAsync(user, hashedPassword);
        return Ok();
    }

    /// <summary>
    ///     토큰재발급
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(JwtResponseModel), 200)]
    public async Task<IActionResult> RefreshToken()
    {
        var headerValidation = HttpRequestHeaderHelper.HeaderValidationAndGetHeader(
            HeaderType.Bearer,
            Request
        );
        if (!headerValidation.isSuccess)
            return Unauthorized("Invalid client request");
        var refreshToken = headerValidation.headerValue;

        var refreshTokenFromDb = await _authService.FindOneByRefreshTokenAsync(refreshToken);

        if (refreshTokenFromDb == null)
            return BadRequest("Invalid client request");

        //토큰 생성
        var newTokens = _jwtHelper.GetJwtSecurityToken(refreshTokenFromDb.userId);

        //사용중인 리프레쉬토큰의 유효기간이 끝나지 않았다면 사용기간을 연장함.
        //유효기간이 끝났다면 새로운 토큰으로 대체함.
        await _authService.RenewRefreshTokenAsync(refreshTokenFromDb, newTokens.refreshToken);

        return Ok(new JwtResponseModel(newTokens.accessToken, newTokens.refreshToken));
    }

    /// <summary>
    ///     로그인
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Login()
    {
        if (
            !HttpRequestHeaderHelper
                .HeaderValidationAndGetHeader(HeaderType.Basic, Request)
                .isSuccess
        )
            return Unauthorized();

        var authSplit = HttpRequestHeaderHelper.AuthSplit(Request);

        var userId = authSplit[0];
        var password = authSplit[1];

        var user = await _authService.FindOneAsync(userId);

        if (user == null)
            return Unauthorized("아이디 또는 비밀번호가 일치하지 않습니다.");

        var hasher = new PasswordHasher<string>();
        if (
            hasher.VerifyHashedPassword(null, user.password!, password)
            != PasswordVerificationResult.Success
        )
            return Unauthorized("아이디 또는 비밀번호가 일치하지 않습니다.");

        if (!user.isValid)
            return Unauthorized("유효하지 않은 아이디입니다.");

        var tokens = _jwtHelper.GetJwtSecurityToken(user.userId);

        await _authService.AddRefreshTokenAsync(tokens.refreshToken, user.userId);

        return Ok(new JwtResponseModel(tokens.accessToken, tokens.refreshToken));
    }
}
