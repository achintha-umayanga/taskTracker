using taskTracker.Application.Common.Interfaces;
using taskTracker.Application.DTO;
using taskTracker.Domain.Entities;

namespace taskTracker.Application.Commands;

public class UpdateTaskCommand
{
  private readonly ITaskRepository _taskRepository;
  private readonly IUserRepository _userRepository;

  public UpdateTaskCommand(
    ITaskRepository taskRepository,
    IUserRepository userRepository
  )
  {
    _taskRepository = taskRepository;
    _userRepository = userRepository;
  }

  public async Task<TaskResponse> UpdateTask(Guid userId, Guid taskId, UpdateTaskRequest request)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);
    if (user is null)
    {
      throw new InvalidOperationException("User not found");
    }

    var task = await _taskRepository.GetTaskById(taskId);
    if (task is null)
    {
      throw new InvalidOperationException("Task not found");
    }

    task.Update(
      request.Name,
      request.Status,
      request.DueDate,
      request.Priority
    );

    await _taskRepository.UpdateTaskItemAsync(task);

    return new TaskResponse(
      task.Id,
      task.Key,
      task.Name,
      task.Status,
      task.DueDate,
      task.Priority,
      task.UserId
    );
  }
}