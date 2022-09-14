using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class InvalidCourseDescriptionException : CourseLibraryException
{
    public AggregateId CourseId { get; }
    public string Description { get; }
    
    public InvalidCourseDescriptionException(AggregateId courseId, string description) 
        : base($"Description: '{description}' is invalid for course with ID: '{courseId}'")
    {
        CourseId = courseId;
        Description = description;
    }
}
