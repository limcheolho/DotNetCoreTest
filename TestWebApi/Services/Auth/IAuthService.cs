using TestWebApi.DataContext.Models;

namespace TestWebApi.Services.Auth;

public interface IAuthService
{
    public Task CreateUserAsync(User user, string hashedPassword);
    public Task UpdateUserPasswordAsync(User user);
    public Task FindUserAsync(string userId);
    

}