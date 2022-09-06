using CourseLibrary.Modules.Students.Core.Events;
using CourseLibrary.Modules.Students.Core.Exceptions;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Modules.Students.Core.Commands.Handlers;

internal sealed class LockStudentHandler : ICommandHandler<LockStudent>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<LockStudentHandler> _logger;
    
    public LockStudentHandler(IStudentRepository studentRepository, IMessageBroker messageBroker, ILogger<LockStudentHandler> logger)
    {
        _studentRepository = studentRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(LockStudent command, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetAsync(command.StudentId);

        if (student is null)
        {
            throw new StudentNotFoundException(command.StudentId);
        }

        if (!student.IsActive)
        {
            return;
        }
        
        student.Lock(command.Notes);

        await _studentRepository.UpdateAsync(student);
        await _messageBroker.PublishAsync(new StudentLocked(command.StudentId));
        
        _logger.LogInformation($"Locked a student with ID: '{command.StudentId}'.");
    }
}
