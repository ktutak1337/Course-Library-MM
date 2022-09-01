using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : CourseLibraryException
{
    public InvalidCredentialsException() 
        : base("Invalid credentials.") { }
}
