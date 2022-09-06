using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Students.Core.Exceptions;

public class StudentNotFoundException : CourseLibraryException
{
    public Guid StudentId { get; }
    
    public StudentNotFoundException(Guid studentId) 
        : base($"Student with ID: '{studentId}' was not found.")
    {
        StudentId = studentId;
    }
}
