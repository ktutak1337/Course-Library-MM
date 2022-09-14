using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

public class InvalidFullNameException : CourseLibraryException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) 
        : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}
