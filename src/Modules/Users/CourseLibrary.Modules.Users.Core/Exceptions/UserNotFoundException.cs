using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class UserNotFoundException : CourseLibraryException
{
    public Guid UserId { get; }
    public string Email { get; }

    public UserNotFoundException(Guid userId) 
        : base($"User with ID: '{userId}' was not found.") 
    {
        UserId = userId;
    }
        
    public UserNotFoundException(string email) 
        : base($"User with email: '{email}' was not found.")
    {
        Email = email;
    }
}
