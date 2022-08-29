using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class UserNotActiveException : CourseLibraryException
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}
