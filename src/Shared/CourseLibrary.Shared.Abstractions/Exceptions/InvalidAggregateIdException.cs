namespace CourseLibrary.Shared.Abstractions.Exceptions;

public class InvalidAggregateIdException : CourseLibraryException
{
    public Guid Id { get; }

    public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate id: {id}")
        => Id = id;
}
