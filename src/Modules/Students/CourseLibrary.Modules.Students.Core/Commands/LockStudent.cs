using CourseLibrary.Shared.Abstractions.Commands;

namespace CourseLibrary.Modules.Students.Core.Commands;

public record LockStudent(Guid StudentId, string Notes = null) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
