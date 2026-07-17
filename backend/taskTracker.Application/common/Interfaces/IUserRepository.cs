using taskTracker.Domain.Entities;

namespace taskTracker.Application.Common.Interfaces;

public interface IUserRepository
{
  Task<bool> IsEmailUniqueAsync(string email);
  Task AddUserAsync(User user);
  Task<User?> GetUserByEmailAsync(string email);
  Task<User?> GetUserByIdAsync(Guid id);
}