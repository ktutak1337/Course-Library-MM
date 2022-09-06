using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Students.Core.Events;

internal record StudentLocked(Guid StudentId) : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
