﻿namespace TestWebApi.DataContext.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable(p => p.HasComment("투두"));
        builder.HasKey(g => g.todoNo);
        builder.HasIndex(g => new { g.todoNo, g.createdAt });
        builder.Property(g => g.todoNo).HasComment("투두번호").ValueGeneratedOnAdd().IsRequired();
        builder.Property(g => g.userId).HasComment("사용자 아이디").IsRequired();
        builder.Property(g => g.title).HasComment("타이틀").HasMaxLength(200).IsRequired();
        builder.Property(g => g.contents).HasComment("콘텐츠").HasMaxLength(500);
        builder.HasOne<User>(g => g.user).WithMany(g => g.todos).HasForeignKey(g => g.userId);
    }
}