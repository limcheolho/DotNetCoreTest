using Microsoft.EntityFrameworkCore;
using TestWebApi.DataContext.Configurations;
using TestWebApi.DataContext.Models;

namespace TestWebApi.DataContext;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public DbSet<ApiLog> ApiLogs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    public DbSet<SchedulerLog> SchedulerLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApiLogConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new TodoConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExceptionLogConfiguration());
        modelBuilder.ApplyConfiguration(new SchedulerLogConfiguration());
    }
}
