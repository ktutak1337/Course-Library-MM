using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class MissingPasswordException : CourseLibraryException
{
    public MissingPasswordException() 
        : base($"Invalid password") { }
}
