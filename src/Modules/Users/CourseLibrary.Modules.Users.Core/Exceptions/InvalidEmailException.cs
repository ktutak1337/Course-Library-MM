using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class InvalidEmailException : CourseLibraryException
{
    public string Email { get; }

    public InvalidEmailException(string email) 
        : base($"The email is invalid: '{email}'.") 
            => Email = email;
}
