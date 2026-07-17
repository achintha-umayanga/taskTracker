using Microsoft.EntityFrameworkCore;
using taskTracker.Application.Common.Interfaces;
using taskTracker.Domain.Entities;
using taskTracker.Infrastructure.Persistence;

namespace taskTracker.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
  private readonly AppDbContext _context;

  public TaskRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task CreateTaskItemAsync(TaskItem taskItem)
  {
    _context.Tasks.Add(taskItem);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateTaskItemAsync(TaskItem taskItem)
  {
    _context.Tasks.Update(taskItem);
    await _context.SaveChangesAsync();
  }

  public async Task<TaskItem?> GetTaskById(Guid id)
  {
    return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
  }

  public async Task DeleteTaskItemAsync(TaskItem taskItem)
  {
    _context.Tasks.Remove(taskItem);
    await _context.SaveChangesAsync();
  }

  public async Task<List<TaskItem?>> GetAllTasks(Guid id)
  {
    return await _context.Tasks.Where(t => t.UserId == id).ToListAsync();
  }
}