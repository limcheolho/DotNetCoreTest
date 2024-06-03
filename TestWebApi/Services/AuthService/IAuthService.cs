namespace TestWebApi.Services.AuthService;

public interface IAuthService
{
    /// <summary>
    /// 사용자 생성
    /// </summary>
    /// <param name="user"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public Task CreateUserAsync(User user, string hashedPassword);

    /// <summary>
    /// 비밀번호 변경
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task UpdateUserPasswordAsync(User user);

    /// <summary>
    /// 사용자 리스트 조회
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userNm"></param>
    /// <returns></returns>
    public Task<IEnumerable<User>> FindUsersAsync(string? userId, string? userNm);

    /// <summary>
    /// id 유효성 검사
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
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
    /// refreshToken으로 사용자 찾기
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public Task<RefreshToken?> FindOneByRefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 유효기간이 지난 리프레쉬토큰 삭제
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteExpiredRefreshTokenAsync();

    /// <summary>
    /// 사용자 정보 업데이트
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task UpdateUserAsync(User user);
}