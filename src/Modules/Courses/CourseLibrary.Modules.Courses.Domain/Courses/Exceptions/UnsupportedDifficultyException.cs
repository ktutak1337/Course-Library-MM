using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class UnsupportedDifficultyException : CourseLibraryException
{
    public string Difficulty { get; }

    public UnsupportedDifficultyException(string difficulty)
        : base($"Nationality: '{difficulty}' is unsupported.")
    {
        Difficulty = difficulty;
    }
}
