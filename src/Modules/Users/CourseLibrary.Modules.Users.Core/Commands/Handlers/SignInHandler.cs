﻿using CourseLibrary.Modules.Users.Core.Events;
using CourseLibrary.Modules.Users.Core.Exceptions;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Modules.Users.Core.Services;
using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Messaging;
using CourseLibrary.Shared.Infrastructure;
using CourseLibrary.Shared.Infrastructure.Auth.JWT;
using CourseLibrary.Shared.Infrastructure.Security;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Modules.Users.Core.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();
    private readonly IUserRepository _userRepository;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInHandler> _logger;

    public SignInHandler(IUserRepository userRepository, IJsonWebTokenManager jsonWebTokenManager,
        IPasswordManager passwordManager, ITokenStorage tokenStorage, IMessageBroker messageBroker,
        ILogger<SignInHandler> logger)
    {
        _userRepository = userRepository;
        _jsonWebTokenManager = jsonWebTokenManager;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        if (command.Email.IsEmpty() || !EmailAddressAttribute.IsValid(command.Email))
        {
            throw new InvalidEmailException(command.Email);
        }

        if (command.Password.IsEmpty())
        {
            throw new MissingPasswordException();
        }

        var user = await _userRepository.GetAsync(command.Email.ToLowerInvariant());
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.IsValid(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        if (!user.IsActive)
        {
            throw new UserNotActiveException(user.Id);
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };

        var jwt = _jsonWebTokenManager.CreateToken(user.Id.ToString(), user.Email, user.Role.Name, claims: claims);
        jwt.Email = user.Email;

        await _messageBroker.PublishAsync(new SignedIn(user.Id));

        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        _tokenStorage.Set(jwt);
    }
}
