namespace CourseLibrary.Modules.Students.Core.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
}
