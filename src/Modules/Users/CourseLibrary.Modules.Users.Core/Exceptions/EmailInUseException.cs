using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class EmailInUseException : CourseLibraryException
{
    public EmailInUseException() 
        : base("Email is already in use.") { }
}
