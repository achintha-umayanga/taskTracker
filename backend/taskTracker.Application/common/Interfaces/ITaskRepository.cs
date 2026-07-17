using taskTracker.Domain.Entities;

namespace taskTracker.Application.Common.Interfaces;

public interface ITaskRepository
{
  Task CreateTaskItemAsync(TaskItem taskItem);
  Task UpdateTaskItemAsync(TaskItem taskItem);
  Task<TaskItem?> GetTaskById(Guid id);
  Task DeleteTaskItemAsync(TaskItem taskItem);
  Task<List<TaskItem?>> GetAllTasks(Guid id);
}