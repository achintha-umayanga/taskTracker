using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using taskTracker.Application.Commands;
using taskTracker.Application.DTO;

namespace taskTracker.Api.Controllers;

[ApiController]
[Route("/api/task")]
[Authorize]
public class TaskController : ControllerBase
{
  private readonly CreateTaskCommand _createTaskCommand;
  private readonly UpdateTaskCommand _updateTaskCommand;
  private readonly DeleteTaskCommand _deleteTaskCommand;
  private readonly GetAllTasksCommand _getAllTAsksCommand;

  public TaskController(
    CreateTaskCommand createTaskCommand,
    UpdateTaskCommand updateTaskCommand,
    DeleteTaskCommand deleteTaskCommand,
    GetAllTasksCommand getAllTAsksCommand
  )
  {
    _createTaskCommand = createTaskCommand;
    _updateTaskCommand = updateTaskCommand;
    _deleteTaskCommand = deleteTaskCommand;
    _getAllTAsksCommand = getAllTAsksCommand;
  }

  private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

  [HttpPost("createTask")]
  public async Task<IActionResult> CreateTask(CreateTaskRequest request)
  {
    var task = await _createTaskCommand.CreateTask(CurrentUserId, request);
    return Ok(task);
  }

  [HttpPut("updateTask/{id:guid}")]
  public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest request)
  {
    await _updateTaskCommand.UpdateTask(CurrentUserId, id, request);
    return Ok(new { message = "Task updated successfully" });
  }

  [HttpDelete("deleteTask/{id:guid}")]
  public async Task<IActionResult> DeleteTask(Guid id)
  {
    await _deleteTaskCommand.DeleteTask(CurrentUserId, id);
    return Ok(new { message = "Task deleted successfully" });
  }

  [HttpGet("getAllTasks")]
  public async Task<IActionResult> GetAllTasks([FromQuery] string? searchTerm)
  {
    var result = await _getAllTAsksCommand.GetAllTasks(CurrentUserId, searchTerm);
    return Ok(result);
  }
}