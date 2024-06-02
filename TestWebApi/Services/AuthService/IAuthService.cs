namespace TestWebApi.Services.AuthService;

public interface IAuthService
{
    public Task CreateUserAsync(User user, string hashedPassword);
    public Task UpdateUserPasswordAsync(User user);
    public Task<IEnumerable<User>> FindUsersAsync(string userId, string userNm);
    
    public Task<User?> FindOneAsync(string userId);

    

    /// <summary>
    /// 유효기간이 지난 리프레쉬토큰 삭제
    /// </summary>
    /// <returns></returns>
    public Task<int> DeleteExpiredRefreshTokenAsync();
    
    public Task UpdateUserAsync(User user);
    
}