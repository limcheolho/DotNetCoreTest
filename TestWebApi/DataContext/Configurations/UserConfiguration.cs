using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestWebApi.DataContext.Models;

namespace TestWebApi.DataContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(g => g.HasComment("사용자 정보"));
        builder.HasKey(g => new { g.userId });
        builder.Property(g => g.userId).IsRequired().HasMaxLength(20).HasComment("아이디");
        builder.Property(g => g.userName).HasMaxLength(20).HasComment("이름");

        builder.Property(g => g.email).HasMaxLength(50).HasComment("이메일");
        builder.Property(g => g.password).IsRequired().HasMaxLength(500).HasComment("비밀번호");
        builder.Property(g => g.phoneNumber).HasMaxLength(20).HasComment("전화번호");
        builder.Property(g => g.zipCode).HasMaxLength(10).HasComment("우편번호");
        builder.Property(g => g.address1).HasMaxLength(100).HasComment("주소1");
        builder.Property(g => g.address2).HasMaxLength(200).HasComment("주소2");

        builder.Property(g => g.totalTodos).HasComment("투두토탈");
    }
}