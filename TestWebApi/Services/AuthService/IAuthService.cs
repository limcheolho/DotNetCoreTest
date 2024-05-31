using TestWebApi.DataContext.Models;

namespace TestWebApi.Services.Auth;

public interface IAuthService
{
    public Task CreateUserAsync(User user, string hashedPassword);
    public Task UpdateUserPasswordAsync(User user);
    public Task FindUserAsync(Task<User?> FindUserAsync);

    /// <summary>
    /// 유효기간이 지난 리프레쉬토큰 삭제
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteExpiredRefreshToken();
}
