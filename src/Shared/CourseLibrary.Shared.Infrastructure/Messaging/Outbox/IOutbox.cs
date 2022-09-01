using CourseLibrary.Shared.Abstractions.Messaging;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Outbox;

internal interface IOutbox
{
    bool Enabled { get; }
    Task SaveAsync(params IMessage[] messages);
    Task PublishUnsentAsync();
}
