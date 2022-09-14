using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

internal class InvalidEmailAddressException : CourseLibraryException
{
    public string Email { get; }
    
    public InvalidEmailAddressException(string email) 
        : base($"Invalid email address: '{email}'")
    {
        Email = email;
    }
}
