namespace CourseLibrary.Modules.Students.Core.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    public Student(Guid id, string email, string fullName, string bio, string avatarUrl, DateTime createdAt)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        Bio = bio;
        AvatarUrl = avatarUrl;
        CreatedAt = createdAt;
    }
}
