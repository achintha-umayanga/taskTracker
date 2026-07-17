using taskTracker.Application.Common.Interfaces;

namespace taskTracker.Application.Commands;

public class DeleteTaskCommand
{
  private readonly ITaskRepository _taskRepository;
  private readonly IUserRepository _userRepository;

  public DeleteTaskCommand(
    ITaskRepository taskRepository,
    IUserRepository userRepository
  )
  {
    _taskRepository = taskRepository;
    _userRepository = userRepository;
  }

  public async Task DeleteTask(Guid userId, Guid taskId)
  {
    var user = _userRepository.GetUserByIdAsync(userId);
    if (user is null)
    {
      throw new InvalidOperationException("User not found");
    }

    var task = await _taskRepository.GetTaskById(taskId);
    if (task is null)
    {
      throw new InvalidOperationException("Task not found");
    }

    await _taskRepository.DeleteTaskItemAsync(task);
  }
}