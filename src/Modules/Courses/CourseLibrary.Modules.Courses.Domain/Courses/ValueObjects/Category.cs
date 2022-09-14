using CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.ValueObjects;

public record Category
{
    private static readonly HashSet<string> AllowedValues = new()
    {
        "Business", "Graphics", "Photography", "Programming", "Marketing", "Audio", "Video", "Personal development"
    };
    
    public string Value { get; }
    
    public Category(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidCategoryException(value);
        }

        if (!AllowedValues.Contains(value))
        {
            throw new UnsupportedCategoryException(value);
        }
        
        Value = value;
    }
    
    public static implicit operator string(Category category) => category.Value;
    public static implicit operator Category(string category) => new Category(category);
}
