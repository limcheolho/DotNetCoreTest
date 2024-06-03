namespace TestWebApi;

public class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
{
    public void Configure(EntityTypeBuilder<ExceptionLog> builder)
    {
        builder.ToTable(g => g.HasComment("Exception로그"));
        builder.HasKey(g => new { g.logNo });

        builder.Property(g => g.logNo).IsRequired().ValueGeneratedOnAdd().HasColumnOrder(0).HasComment("로그순번");
        builder.Property(g => g.controllerName).HasColumnOrder(1).HasComment("controller name");
        builder.Property(g => g.actionName).HasColumnOrder(2).HasComment("action");
        builder.Property(g => g.methodName).HasColumnOrder(3).HasComment("methodName");
        builder.Property(g => g.message).HasColumnOrder(4).HasComment("message");
        builder.Property(g => g.stackTrace).HasColumnOrder(5).HasComment("stackTrace");
    }
}
