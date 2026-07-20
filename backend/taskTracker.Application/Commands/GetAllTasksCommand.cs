using taskTracker.Application.Common.Interfaces;
using taskTracker.Application.DTO;
using taskTracker.Domain.Entities;

namespace taskTracker.Application.Commands;

public class GetAllTasksCommand
{
  private readonly ITaskRepository _taskRepository;
  private readonly IUserRepository _userRepository;

  public GetAllTasksCommand(
    ITaskRepository taskRepository,
    IUserRepository userRepository
  )
  {
    _taskRepository = taskRepository;
    _userRepository = userRepository;
  }

  public async Task<List<TaskResponse>> GetAllTasks(Guid userId, string? searchTerm)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);
    if (user is null)
    {
      throw new InvalidOperationException("User not found");
    }

    var tasks = await _taskRepository.GetAllTasks(userId, searchTerm);

    return tasks
      .Select(t => new TaskResponse(
        t.Id,
        t.Key,
        t.Name,
        t.Status,
        t.DueDate,
        t.Priority,
        t.UserId
      )).ToList();
  }
}