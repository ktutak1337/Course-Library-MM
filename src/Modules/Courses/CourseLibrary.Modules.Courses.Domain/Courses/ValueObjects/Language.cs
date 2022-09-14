using CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.ValueObjects;

public record Language
{
    private static readonly HashSet<string> AllowedValues = new()
    {
        "PL", "EN", "ES", "DE", "FR", "IT"
    };
    
    public string Value { get; }
    
    public Language(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 2)
        {
            throw new InvalidLanguageException(value);
        }

        value = value.ToUpperInvariant();

        if (!AllowedValues.Contains(value))
        {
            throw new UnsupportedLanguageException(value);
        }
        
        Value = value;
    }
    
    public static implicit operator string(Language language) => language.Value;
    public static implicit operator Language(string language) => new Language(language);
}