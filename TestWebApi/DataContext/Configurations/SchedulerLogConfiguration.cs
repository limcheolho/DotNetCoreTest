namespace TestWebApi.DataContext.Configurations;

public class SchedulerLogConfiguration : IEntityTypeConfiguration<SchedulerLog>
{
    public void Configure(EntityTypeBuilder<SchedulerLog> builder)
    {
        builder.ToTable(g => g.HasComment("스케줄러 로그"));
        builder.HasKey(g => new { g.jobNo });
        builder.HasIndex(e => e.createdAt);
        builder.Property(g => g.jobNo).IsRequired().ValueGeneratedOnAdd().HasComment("작업번호");
        builder.Property(g => g.jobType).IsRequired().HasMaxLength(50).HasComment("작업타입");
        builder.Property(g => g.resultType).HasMaxLength(10).HasComment("작업결과타입");
        builder.Property(g => g.remarks).HasComment("비고");
    }
}
