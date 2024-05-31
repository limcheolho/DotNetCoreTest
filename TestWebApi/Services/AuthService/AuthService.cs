using Microsoft.Extensions.Options;

namespace TestWebApi.Services.Auth;

public class AuthService : IAuthService
{
    private readonly TestDbContext _dbContext;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly SystemInfoExtensions _systemInfoExtensions;

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

    public Task<int> DeleteExpiredRefreshToken()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> FindUserAsync(string userId) =>
        await _dbContext.Users.FirstOrDefaultAsync(x => x.userId.Trim() == userId.Trim());

    public async Task UpdateUserPasswordAsync(User user)
    {
        var data = await FindUserAsync(userId: user.userId);
        data!.password = user.password;
        _systemInfoExtensions.SetModelLogInfo(data);
        await _dbContext.SaveChangesAsync();
    }
}
