using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class InvalidUserStateException : CourseLibraryException
{
    private string? State { get; }
    
    public InvalidUserStateException(string state) 
        : base($"User state is invalid: '{state}'.") { }
}
