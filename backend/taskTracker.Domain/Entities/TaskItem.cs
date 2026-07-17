using taskTracker.Domain.Common;
using taskTracker.Domain.Models;

namespace taskTracker.Domain.Entities;

public class TaskItem : BaseEntity
{
  public string Key { get; set; }
  public string Name { get; set; }
  public Status Status { get; set; }
  // public List<string> Assignees { get; set; }
  public DateOnly DueDate { get; set; }
  public Priority Priority { get; set; }
  public User User { get; set; }
  public Guid UserId { get; set; }

  private TaskItem() {}

  public static TaskItem Create(
    string key,
    string name,
    Status status,
    DateOnly dueDate,
    Priority priority,
    Guid userId
  )
  {
    return new TaskItem
    {
      Key = key,
      Name = name,
      Status = status,
      DueDate = dueDate,
      Priority = priority,
      UserId = userId
    };
  }

  public void Update(
    string name,
    Status status,
    DateOnly dueDate,
    Priority priority
  )
  {
    Name = name;
    Status = status;
    DueDate = dueDate;
    Priority = priority;
  }
}