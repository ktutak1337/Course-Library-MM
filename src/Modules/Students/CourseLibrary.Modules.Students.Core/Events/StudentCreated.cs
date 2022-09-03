using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Students.Core.Events;

public record StudentCreated(Guid StudentId, string Email, string FullName) : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
