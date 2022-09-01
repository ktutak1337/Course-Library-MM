namespace CourseLibrary.Shared.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PublishAsync(params IMessage[] messages);
}
