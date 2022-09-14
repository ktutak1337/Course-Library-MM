using CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Types;

public record ModuleId
{
    public Guid Value { get; }

    public ModuleId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static ModuleId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ModuleId value)
        => value.Value;
    
    public static implicit operator ModuleId(Guid value)
        => new(value);
    
    public override string ToString() => Value.ToString("N");
}
