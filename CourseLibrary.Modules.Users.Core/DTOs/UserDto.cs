namespace CourseLibrary.Modules.Users.Core.DTOs;

internal class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
