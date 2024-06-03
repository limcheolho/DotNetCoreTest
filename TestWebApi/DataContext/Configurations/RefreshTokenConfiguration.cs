namespace TestWebApi.DataContext.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(g => g.HasComment("Refresh Token 정보"));
        builder.HasKey(d => new { d.userId, d.refreshToken });
        builder.HasIndex(d => new { d.refreshToken });

        builder.Property(g => g.refreshToken).IsRequired().HasMaxLength(500).HasComment("refreshToken");
        builder.Property(g => g.userId).IsRequired().HasMaxLength(20).HasComment("아이디");
        builder.Property(g => g.refreshTokenExpiryTime).IsRequired().HasComment("토큰유효일시");
        builder.HasOne<User>(g => g.user).WithMany(g => g.refreshTokens).HasForeignKey(g => g.userId);
    }
}