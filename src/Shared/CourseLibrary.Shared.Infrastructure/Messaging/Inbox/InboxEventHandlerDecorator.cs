using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Inbox;

[Decorator]
internal class InboxEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IInbox _inbox;
    private readonly string _module;

    public InboxEventHandlerDecorator(IEventHandler<T> handler, IInbox inbox)
    {
        _handler = handler;
        _inbox = inbox;
        _module = _handler.GetModuleName();
    }

    public Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        => _inbox.Enabled
            ? _inbox.HandleAsync(@event, () => _handler.HandleAsync(@event, cancellationToken), _module)
            : _handler.HandleAsync(@event);
}
