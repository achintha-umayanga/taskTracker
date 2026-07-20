using taskTracker.Domain.Models;

namespace taskTracker.Application.DTO;

public record TaskResponse
(
  Guid Id,
  string Key,
  string Name,
  Status Status,
  DateOnly DueDate,
  Priority Priority,
  Guid UserId
);