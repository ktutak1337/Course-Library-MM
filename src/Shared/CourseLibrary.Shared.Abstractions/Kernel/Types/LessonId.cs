using CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Types;

public record LessonId
{
    public Guid Value { get; }

    public LessonId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static LessonId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(LessonId value)
        => value.Value;
    
    public static implicit operator LessonId(Guid value)
        => new(value);
    
    public override string ToString() => Value.ToString("N");
}
