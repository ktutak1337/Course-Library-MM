using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Students.Core.Events.External;

public record SignedUp(Guid UserId, string Email, string Role) : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
