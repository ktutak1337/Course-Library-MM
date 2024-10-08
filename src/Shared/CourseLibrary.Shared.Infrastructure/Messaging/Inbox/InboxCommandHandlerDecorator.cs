﻿using CourseLibrary.Shared.Abstractions.Commands;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Inbox;

[Decorator]
internal class InboxCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IInbox _inbox;
    private readonly string _module;

    public InboxCommandHandlerDecorator(ICommandHandler<T> handler, IInbox inbox)
    {
        _handler = handler;
        _inbox = inbox;
        _module = _handler.GetModuleName();
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
        => _inbox.Enabled
            ? _inbox.HandleAsync(command, () => _handler.HandleAsync(command, cancellationToken), _module)
            : _handler.HandleAsync(command);
}
