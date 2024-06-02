using Microsoft.Extensions.Options;

namespace TestWebApi.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly TestDbContext _dbContext;
    private readonly SystemInfoExtensions _systemInfoExtensions;

    private readonly IOptions<JwtSettings> _jwtSettings;

    public AuthService(
        TestDbContext dbContext,
        IOptions<JwtSettings> jwtSettings,
        SystemInfoExtensions systemInfoExtensions
    )
    {
        _dbContext = dbContext;
        _jwtSettings = jwtSettings;
        _systemInfoExtensions = systemInfoExtensions;
    }

    public async Task CreateUserAsync(User user, string hashedPassword)
    {
        _systemInfoExtensions.SetModelLogInfo(user);
        user.password = hashedPassword;
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> FindUsersAsync(string userId, string userNm)
    {
        var data = await _dbContext
            .Users.Where(g =>
                (
                    string.IsNullOrEmpty(userId)
                    || !string.IsNullOrEmpty(userId) && g.userId == userId
                )
                && (
                    string.IsNullOrEmpty(userNm)
                    || !string.IsNullOrEmpty(userNm) && g.userName.Contains(userNm)
                )
            )
            .AsNoTracking()
            .ToListAsync();

        return data;
    }


    public async Task<IEnumerable<User>> FindUsersAsync(string userId, string userNm)
    {
        var data = await _dbContext
            .Users.Where(g =>
                (
                    string.IsNullOrEmpty(userId)
                    || !string.IsNullOrEmpty(userId) && g.userId == userId
                )
                && (
                    string.IsNullOrEmpty(userNm)
                    || !string.IsNullOrEmpty(userNm) && g.userName.Contains(userNm)
                )
            )
            .AsNoTracking()
            .ToListAsync();


        return data;
    }

    public async Task<int> DeleteExpiredRefreshTokenAsync()
    {
        var data = await _dbContext
            .RefreshTokens.Where(g => g.refreshTokenExpiryTime < DateTime.Now)
            .ToListAsync();
        var cnt = data.Count;
        _dbContext.RefreshTokens.RemoveRange(data);
        await _dbContext.SaveChangesAsync();
        return cnt;
    }

    public async Task UpdateUserPasswordAsync(User user)
    {
        var data = await FindOneAsync(userId: user.userId);
        data!.password = user.password;
        _systemInfoExtensions.SetModelLogInfo(data);
        await _dbContext.SaveChangesAsync();
    }


    public async Task UpdateUserAsync(User user)
    {
        var data = await FindOneAsync(userId: user.userId);

        data!.userName = user.userName;
        data.email = user.email;
        data.phoneNumber = user.phoneNumber;
        data.zipCode = user.zipCode;
        data.address1 = user.address1;
        data.address2 = user.address2;
        data.isValid = user.isValid;
        _systemInfoExtensions.SetModelLogInfo(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> FindOneAsync(string userId) =>
        await _dbContext.Users.FirstOrDefaultAsync(x => x.userId.Trim() == userId.Trim());
}