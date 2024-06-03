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

    public async Task<IEnumerable<User>> FindUsersAsync(string? userId, string? userNm)
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
            .Include(g => g.todos)
            .Include(g => g.refreshTokens)
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

    //리프레쉬 토큰 저장
    public async Task AddRefreshTokenAsync(string refreshToken, string userId)
    {
        var newRefresh = new RefreshToken()
        {
            userId = userId,
            refreshTokenExpiryTime = DateTime.Now.AddDays(
                _jwtSettings.Value.RefreshTokenValidityInDays
            ),
            refreshToken = refreshToken,
        };
        _systemInfoExtensions.SetModelLogInfo(newRefresh);

        await _dbContext.RefreshTokens.AddAsync(newRefresh);
        await _dbContext.SaveChangesAsync();
    }

    //리프레쉬 토큰 갱신
    public async Task RenewRefreshTokenAsync(RefreshToken refresh, string newRefreshToken)
    {
        var newRefresh = new RefreshToken()
        {
            userId = refresh.userId,
            refreshTokenExpiryTime = DateTime.Now.AddDays(
                _jwtSettings.Value.RefreshTokenValidityInDays
            ),
            refreshToken = newRefreshToken,
        };
        _systemInfoExtensions.SetModelLogInfo(newRefresh);
        //기존 리프레쉬 토큰 삭제
        _dbContext.RefreshTokens.Remove(refresh);
        //신규 리프레쉬 토큰 추가
        await _dbContext.RefreshTokens.AddAsync(newRefresh);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> FindOneByRefreshTokenAsync(string refreshToken)
    {
        var data = await _dbContext
            .RefreshTokens.Include(g => g.user)
            .FirstOrDefaultAsync(g => g.refreshToken == refreshToken && g.isValid);
        return data;
    }
}
