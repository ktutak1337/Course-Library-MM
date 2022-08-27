﻿using CourseLibrary.Shared.Abstractions.Messaging;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Inbox;

internal interface IInbox
{
    bool Enabled { get; }
    Task HandleAsync(IMessage message, Func<Task> handler, string module);
}
