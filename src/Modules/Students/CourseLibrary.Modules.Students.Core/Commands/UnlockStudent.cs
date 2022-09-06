using CourseLibrary.Shared.Abstractions.Commands;

namespace CourseLibrary.Modules.Students.Core.Commands;

public record UnlockStudent(Guid StudentId, string Notes = null) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
