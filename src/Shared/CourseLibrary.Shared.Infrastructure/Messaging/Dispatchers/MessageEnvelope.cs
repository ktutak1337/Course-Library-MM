using CourseLibrary.Shared.Abstractions.Messaging;

namespace CourseLibrary.Shared.Infrastructure.Messaging.Dispatchers;

public record MessageEnvelope(IMessage Message, IMessageContext MessageContext);

