using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Events;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Shared.Abstractions.Dispatchers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}
