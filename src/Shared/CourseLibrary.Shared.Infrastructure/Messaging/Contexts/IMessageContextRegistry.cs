using CourseLibrary.Shared.Abstractions.Messaging;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}
