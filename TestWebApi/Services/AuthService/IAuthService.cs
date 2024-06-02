namespace TestWebApi.Services.AuthService;

public interface IAuthService
{
    public Task CreateUserAsync(User user, string hashedPassword);
    public Task UpdateUserPasswordAsync(User user);
    public Task<IEnumerable<User>> FindUsersAsync(string? userId, string? userNm);

    public Task<User?> FindOneAsync(string userId);

    /// <summary>
    /// 리프레쉬 토큰 생성
    /// </summary>
    /// <returns></returns>
    public Task AddRefreshTokenAsync(string refreshToken, string userId);

    /// <summary>
    /// 리프레쉬 토큰 갱신
    /// </summary>
    /// <returns></returns>
    public Task RenewRefreshTokenAsync(RefreshToken refresh, string newRefreshToken);

    /// <summary>
    ///     refreshToken으로 사용자 찾기
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public Task<RefreshToken?> FindOneByRefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 유효기간이 지난 리프레쉬토큰 삭제
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteExpiredRefreshTokenAsync();

    public Task UpdateUserAsync(User user);
}
