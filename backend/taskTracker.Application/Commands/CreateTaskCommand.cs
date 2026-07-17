using taskTracker.Application.Common.Interfaces;
using taskTracker.Application.DTO;
using taskTracker.Domain.Entities;

namespace taskTracker.Application.Commands;

public class CreateTaskCommand
{
  private readonly ITaskRepository _taskRepository;
  private readonly IUserRepository _userRepository;

  public CreateTaskCommand(
    ITaskRepository taskRepository,
    IUserRepository userRepository
  )
  {
    _taskRepository = taskRepository;
    _userRepository = userRepository;
  }

  public async Task<TaskResponse> CreateTask(Guid userId, CreateTaskRequest request)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);
    if (user is null)
    {
      throw new InvalidOperationException("User not found");
    }

    var key = "TT-1";

    var taskItem = TaskItem.Create(
      key,
      request.Name,
      request.Status,
      request.DueDate,
      request.Priority,
      userId
    );

    await _taskRepository.CreateTaskItemAsync(taskItem);

    return new TaskResponse(
      taskItem.Key,
      taskItem.Name,
      taskItem.Status,
      taskItem.DueDate,
      taskItem.Priority,
      taskItem.UserId
    );
  }

}