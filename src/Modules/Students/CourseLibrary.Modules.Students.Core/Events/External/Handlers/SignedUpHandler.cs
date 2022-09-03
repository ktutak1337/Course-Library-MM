using CourseLibrary.Modules.Students.Core.Entities;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Events;
using CourseLibrary.Shared.Abstractions.Messaging;
using CourseLibrary.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Modules.Students.Core.Events.External.Handlers;

public class SignedUpHandler : IEventHandler<SignedUp>
{
    private const string ValidRole = "user";

    private readonly IStudentRepository _studentRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignedUpHandler> _logger;
    private readonly IClock _clock;

    public SignedUpHandler(IStudentRepository studentRepository, IMessageBroker messageBroker, ILogger<SignedUpHandler> logger, IClock clock)
    {
        _studentRepository = studentRepository;
        _messageBroker = messageBroker;
        _logger = logger;
        _clock = clock;
    }

    public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
    {
        if (@event.Role is not ValidRole)
        {
            return;
        }

        var email = @event.Email;

        var student = new Student(@event.UserId, email, fullName: email.Split("@").First(), bio: string.Empty, avatarUrl: string.Empty, _clock.CurrentDate());
        await _studentRepository.AddAsync(student);
        _logger.LogInformation($"Created a new student based on user with ID: '{@event.UserId}'.");
        await _messageBroker.PublishAsync(new StudentCreated(student.Id, student.Email, student.FullName));
    }
}
