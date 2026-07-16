namespace taskTracker.Application.DTO;

public record LoginRequest
(
  string Email,
  string Password
);