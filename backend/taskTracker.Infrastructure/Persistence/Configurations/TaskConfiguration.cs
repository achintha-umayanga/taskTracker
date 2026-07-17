using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using taskTracker.Domain.Entities;

namespace taskTracker.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t => t.Key);
    builder.Property(t => t.Name);
    builder.Property(t => t.Status);
    builder.Property(t => t.DueDate);
    builder.Property(t => t.Priority);
    builder.HasOne(t => t.User)
      .WithMany()
      .HasForeignKey(t => t.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}