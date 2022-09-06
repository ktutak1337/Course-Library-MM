using CourseLibrary.Modules.Students.Core.Events;
using CourseLibrary.Modules.Students.Core.Exceptions;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Modules.Students.Core.Commands.Handlers;

internal sealed class UnlockStudentHandler : ICommandHandler<UnlockStudent>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<UnlockStudentHandler> _logger;
    
    public UnlockStudentHandler(IStudentRepository studentRepository, IMessageBroker messageBroker, ILogger<UnlockStudentHandler> logger)
    {
        _studentRepository = studentRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }
    
    public async Task HandleAsync(UnlockStudent command, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetAsync(command.StudentId);

        if (student is null)
        {
            throw new StudentNotFoundException(command.StudentId);
        }
        
        student.Unlock(command.Notes);

        await _studentRepository.UpdateAsync(student);
        await _messageBroker.PublishAsync(new StudentUnlocked(command.StudentId));
        
        _logger.LogInformation($"Unlocked a student with ID: '{command.StudentId}'.");
    }
}
