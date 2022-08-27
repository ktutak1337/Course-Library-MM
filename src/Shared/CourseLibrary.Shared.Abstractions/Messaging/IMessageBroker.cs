namespace CourseLibrary.Shared.Abstractions.Messaging;

public interface IMessageBroker
{
    ValueTask PublishAsync(IMessage message, CancellationToken cancellationToken = default);
    ValueTask PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default);
}
