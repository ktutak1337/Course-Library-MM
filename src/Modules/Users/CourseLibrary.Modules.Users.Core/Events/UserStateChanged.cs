using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Users.Core.Events;

internal record UserStateChanged(Guid UserId, string State) : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
