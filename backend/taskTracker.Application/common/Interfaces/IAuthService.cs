using taskTracker.Application.DTO;

namespace taskTracker.Application.Common.Interfaces;

public interface IAuthService
{
  Task RegisterAsync(RegisterRequest request);
  Task<AuthResponse> LoginAsync(LoginRequest request);
}