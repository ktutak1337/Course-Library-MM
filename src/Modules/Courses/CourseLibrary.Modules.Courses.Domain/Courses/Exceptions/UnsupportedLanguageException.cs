using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class UnsupportedLanguageException : CourseLibraryException
{
    public string Language { get; }

    public UnsupportedLanguageException(string language)
        : base($"Language: '{language}' is unsupported.")
    {
        Language = language;
    }
}
