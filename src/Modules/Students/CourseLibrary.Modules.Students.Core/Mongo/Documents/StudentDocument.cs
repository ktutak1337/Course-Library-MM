using CourseLibrary.Modules.Students.Core.Entities;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Students.Core.Mongo.Documents;

public class StudentDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }


    public StudentDocument(Student student)
    {
        Id = student.Id;
        Email = student.Email;
        FullName = student.FullName;
        Bio = student.Bio;
        AvatarUrl = student.AvatarUrl;
        CreatedAt = student.CreatedAt;
    }

    public Student ToEntity() => new(Id, Email, FullName, Bio, AvatarUrl, CreatedAt);
}
