using taskTracker.Domain.Models;

namespace taskTracker.Application.DTO;

public record UpdateTaskRequest
(
  string Name,
  Status Status,
  DateOnly DueDate,
  Priority Priority
);