using CourseLibrary.Shared.Abstractions.Domain;

namespace CourseLibrary.Shared.Abstractions.Kernel;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}