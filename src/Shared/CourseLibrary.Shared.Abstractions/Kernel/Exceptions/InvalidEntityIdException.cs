using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Shared.Abstractions.Kernel.Exceptions;

public class InvalidEntityIdException : CourseLibraryException
{
    public Guid Id { get; }
    
    public InvalidEntityIdException(Guid id) 
        : base($"Cannot set: {id}  as entity identifier.")
    {
        Id = id;
    }
}
