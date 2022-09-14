using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class InvalidCourseNameException : CourseLibraryException
{
    public AggregateId CourseId { get; }
    public string Name { get; }
    
    public InvalidCourseNameException(AggregateId courseId, string name) 
        : base($"Name: '{name}' is invalid for course with ID: '{courseId}'")
    {
        CourseId = courseId;
        Name = name;
    }
}
