using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Modules.Users.Core.Events;
using CourseLibrary.Modules.Users.Core.Exceptions;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace CourseLibrary.Modules.Users.Core.Commands.Handlers;

internal sealed class UpdateUserStateHandler : ICommandHandler<UpdateUserState>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<UpdateUserStateHandler> _logger;

    public UpdateUserStateHandler(IUserRepository userRepository, IMessageBroker messageBroker, ILogger<UpdateUserStateHandler> logger)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(UpdateUserState command, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<UserState>(command.State, true, out var state))
        {
            throw new InvalidUserStateException(command.State);
        }
        
        var user = await _userRepository.GetAsync(command.UserId);
        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }
        
        var previousState = user.State;
        if (previousState == state)
        {
            return;
        }
        
        if ((user.Role.Name, state) == (Role.Admin, UserState.Locked))
        {
            throw new UserStateCannotBeChangedException(command.State, command.UserId);
        }
        
        user.State = state;
        
        await _userRepository.UpdateAsync(user);
        await _messageBroker.PublishAsync(new UserStateChanged(user.Id, state.ToString().ToLowerInvariant()));
        
        _logger.LogInformation($"Updated state for user with ID: '{user.Id}', from: '{previousState}' -> '{user.State}'.");
    }
}
