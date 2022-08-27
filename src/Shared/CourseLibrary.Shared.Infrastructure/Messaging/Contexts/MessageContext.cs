using CourseLibrary.Shared.Abstractions.Contexts;
using CourseLibrary.Shared.Abstractions.Messaging;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Contexts;

public class MessageContext : IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }

    public MessageContext(Guid messageId, IContext context)
    {
        MessageId = messageId;
        Context = context;
    }
}
