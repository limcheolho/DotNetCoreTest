using Microsoft.EntityFrameworkCore;
using TestWebApi.DataContext.Configurations;
using TestWebApi.DataContext.Models;

namespace TestWebApi;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public DbSet<ApiLog> ApiLogs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApiLogConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}