using taskTracker.Domain.Models;

namespace taskTracker.Application.DTO;

public record CreateTaskRequest
(
  string Name,
  Status Status,
  DateOnly DueDate,
  Priority Priority
);