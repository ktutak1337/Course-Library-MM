using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Students.Core.Events.External;

internal record UserStateChanged(Guid UserId, string State) : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
