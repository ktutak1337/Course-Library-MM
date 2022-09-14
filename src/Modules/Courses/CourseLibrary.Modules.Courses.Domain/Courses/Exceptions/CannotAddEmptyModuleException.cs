using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class CannotAddEmptyModuleException : CourseLibraryException
{
    public AggregateId CourseId { get; }
    
    public CannotAddEmptyModuleException(AggregateId courseId) 
        : base($"Cannot add an empty module to the course with ID: {courseId}")
    {
        CourseId = courseId;
    }
}
