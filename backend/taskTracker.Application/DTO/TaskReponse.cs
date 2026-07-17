using taskTracker.Domain.Models;

namespace taskTracker.Application.DTO;

public record TaskResponse
(
  string Key,
  string Name,
  Status Status,
  DateOnly DueDate,
  Priority Priority,
  Guid UserId
);