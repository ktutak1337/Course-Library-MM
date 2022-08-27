using CourseLibrary.Shared.Abstractions.Contexts;

namespace CourseLibrary.Shared.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}
