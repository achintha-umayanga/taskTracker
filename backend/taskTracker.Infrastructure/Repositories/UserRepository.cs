using Microsoft.EntityFrameworkCore;
using taskTracker.Application.Common.Interfaces;
using taskTracker.Domain.Entities;
using taskTracker.Infrastructure.Persistence;

namespace taskTracker.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }
  public Task<bool> IsEmailUniqueAsync(string email)
  {
    return _context.Users.AllAsync(u => u.Email != email);
  }

  public async Task AddUserAsync(User user)
  {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
  }

  public Task<User?> GetUserByEmailAsync(string email)
  {
    return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }

  public Task<User?> GetUserByIdAsync(Guid id)
  {
    return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
  }
}