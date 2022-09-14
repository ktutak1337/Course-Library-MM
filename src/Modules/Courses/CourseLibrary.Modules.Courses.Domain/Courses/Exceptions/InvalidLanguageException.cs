using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class InvalidLanguageException : CourseLibraryException
{
    public string Language { get; }

    public InvalidLanguageException(string language)
        : base($"Language: '{language}' is invalid.")
    {
        Language = language;
    }
}
