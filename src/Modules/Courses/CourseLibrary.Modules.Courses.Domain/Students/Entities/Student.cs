using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Kernel.ValueObjects;

namespace CourseLibrary.Modules.Courses.Domain.Students.Entities;

public class Student : AggregateRoot
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public bool IsActive { get; private set; }
    
    public Student(FullName fullName, Email email, bool isActive)
    {
        FullName = fullName;
        Email = email;
        IsActive = isActive;
        
        IncrementVersion();
    }
}
