using CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.ValueObjects;

public record Difficulty
{
    private static readonly HashSet<string> AllowedValues = new()
    {
        "Beginners", "Intermediate", "Advanced", "Expert"
    };
    
    public string Value { get; }

    public Difficulty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDifficultyException(value);
        }

        if (!AllowedValues.Contains(value))
        {
            throw new UnsupportedDifficultyException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Difficulty difficulty) => difficulty.Value;
    public static implicit operator Difficulty(string difficulty) => new Difficulty(difficulty);
}
