namespace CourseLibrary.Modules.Users.Core.Entities;

internal class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public User(Guid id, string email, string password, Role role, bool isActive, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
        CreatedAt = createdAt;
    }
}
