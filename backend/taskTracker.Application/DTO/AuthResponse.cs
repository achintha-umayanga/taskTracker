namespace taskTracker.Application.DTO;

public record AuthResponse
(
  Guid Id,
  string UserName,
  string Email,
  string Token
);