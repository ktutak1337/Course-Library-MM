using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Events;
using CourseLibrary.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Modules.Students.Core.Events.External.Handlers;

internal sealed class UserStateChangedHandler : IEventHandler<UserStateChanged>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<UserStateChangedHandler> _logger;

    public UserStateChangedHandler(IStudentRepository studentRepository, IMessageBroker messageBroker, ILogger<UserStateChangedHandler> logger)
    {
        _studentRepository = studentRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(UserStateChanged @event, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetAsync(@event.UserId);

        if (student is null)
        {
            return;
        }
        
        IEvent integrationEvent;
        
        switch (@event.State.ToLowerInvariant())
        {
            case "active":
                student.Unlock();
                integrationEvent = new StudentUnlocked(student.Id);
                break;
            case "locked":
                student.Lock();
                integrationEvent = new StudentLocked(student.Id);
                break;
            default:
                _logger.LogWarning($"Received an unknown user state: '{@event.State}'.");
                return;
        }
        
        await _studentRepository.UpdateAsync(student);
        await _messageBroker.PublishAsync(integrationEvent);
        
        _logger.LogInformation($"{(student.IsActive ? "Unlocked" : "Locked")} " +
                               $"student with ID: '{student.Id}'.");
    }
}
