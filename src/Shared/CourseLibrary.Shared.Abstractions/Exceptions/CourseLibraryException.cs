namespace CourseLibrary.Shared.Abstractions.Exceptions;

public abstract class CourseLibraryException : Exception
{
    protected CourseLibraryException(string message) : base(message)
    {
    }
}
