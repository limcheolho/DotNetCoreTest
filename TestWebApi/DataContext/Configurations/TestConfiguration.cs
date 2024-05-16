using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestWebApi;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable(p => p.HasComment("테스트"));
        builder.HasKey(p => p.column1);

        builder.Property(p => p.column1).HasComment("테스트 코멘트1").IsRequired();
        builder.Property(p => p.column2).HasComment("테스트 코멘트2");
    }
}
