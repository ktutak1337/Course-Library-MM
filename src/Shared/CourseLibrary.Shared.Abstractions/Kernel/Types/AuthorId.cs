using CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Types;

public record AuthorId
{
    public Guid Value { get; }

    public AuthorId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static LessonId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(AuthorId value)
        => value.Value;
    
    public static implicit operator AuthorId(Guid value)
        => new(value);
    
    public override string ToString() => Value.ToString("N");
}
