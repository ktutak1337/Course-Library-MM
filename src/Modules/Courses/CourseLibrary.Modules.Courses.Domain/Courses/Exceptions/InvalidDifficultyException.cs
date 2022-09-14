using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class InvalidDifficultyException : CourseLibraryException
{
    public string Difficulty { get; }

    public InvalidDifficultyException(string difficulty)
        : base($"Difficulty: '{difficulty}' is invalid.")
    {
        Difficulty = difficulty;
    }
}
