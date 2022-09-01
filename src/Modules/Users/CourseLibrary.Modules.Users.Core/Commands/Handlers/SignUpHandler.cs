using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Modules.Users.Core.Events;
using CourseLibrary.Modules.Users.Core.Exceptions;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Messaging;
using CourseLibrary.Shared.Abstractions.Time;
using CourseLibrary.Shared.Infrastructure;
using CourseLibrary.Shared.Infrastructure.Security;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Modules.Users.Core.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();
    private static readonly string DefaultRole = Role.Default;
    private const string DefaultJobTitle = "employee";

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignUpHandler> _logger;

    public SignUpHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        IPasswordManager passwordManager, IClock clock, IMessageBroker messageBroker,
        ILogger<SignUpHandler> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        if (command.Email.IsEmpty() || !EmailAddressAttribute.IsValid(command.Email))
        {
            throw new InvalidEmailException(command.Email);
        }

        if (command.Password.IsEmpty())
        {
            throw new MissingPasswordException();
        }

        var email = command.Email.ToLowerInvariant();
        var user = await _userRepository.GetAsync(email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }

        var roleName = command.Role.IsEmpty() ? DefaultRole : command.Role.ToLowerInvariant();
        var role = await _roleRepository.GetAsync(roleName);
        if (role is null)
        {
            throw new RoleNotFoundException(roleName);
        }

        var now = _clock.CurrentDate();
        var password = _passwordManager.Secure(command.Password);

        user = new User(command.UserId, email, password, role, isActive: true, createdAt: now);

        await _userRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new SignedUp(user.Id, email, role.Name));
        _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}
