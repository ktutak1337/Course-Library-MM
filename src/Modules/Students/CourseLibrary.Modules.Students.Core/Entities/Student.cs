namespace CourseLibrary.Modules.Students.Core.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    private Student() { }
    
    public Student(Guid id, string email, string fullName, string bio, string avatarUrl, string? notes, bool isActive, DateTime createdAt)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        Bio = bio;
        AvatarUrl = avatarUrl;
        Notes = notes;
        IsActive = isActive;
        CreatedAt = createdAt;
    }
    
    public void Lock(string notes = null)
    {
        IsActive = false;
        Notes = notes?.Trim();
    }
        
    public void Unlock(string notes = null)
    {
        IsActive = true;
        Notes = notes?.Trim();
    }
}
