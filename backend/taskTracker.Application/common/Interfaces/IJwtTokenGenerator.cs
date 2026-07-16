using taskTracker.Domain.Entities;

namespace taskTracker.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}