namespace TestWebApi.DataContext.Configurations;

public class ApiLogConfiguration : IEntityTypeConfiguration<ApiLog>
{
    public void Configure(EntityTypeBuilder<ApiLog> builder)
    {
        builder.ToTable(p => p.HasComment("api로그"));
        builder.HasKey(p => p.logNo);
        builder.HasIndex(p => p.createdAt);

        builder.Property(p => p.logNo)
            .IsRequired().ValueGeneratedOnAdd().HasComment("로그순번").HasColumnOrder(0);
        builder.Property(p => p.elapsedSec)
            .HasComment("경과시간").HasColumnOrder(1);
        builder.Property(p => p.controllerName)
            .HasComment("controller name").IsRequired().HasMaxLength(50).HasColumnOrder(2);
        builder.Property(p => p.action)
            .HasComment("action").IsRequired().HasMaxLength(100).HasColumnOrder(3);
        builder.Property(p => p.path)
            .HasComment("path").HasMaxLength(100).HasColumnOrder(4);
        builder.Property(p => p.method)
            .HasComment("method").HasMaxLength(10).HasColumnOrder(5);
        builder.Property(p => p.statusCode)
            .HasComment("http result").HasMaxLength(20).HasColumnOrder(6);
        builder.Property(p => p.userId)
            .HasComment("사용자아이디").HasMaxLength(20).HasColumnOrder(7);
        builder.Property(p => p.reqContents)
            .HasComment("reqContents").HasColumnOrder(8);
        builder.Property(p => p.resContents)
            .HasComment("resContents").HasColumnOrder(9);
        builder.Property(p => p.headerValue)
            .HasComment("headerValue").HasColumnOrder(10);
    }
}