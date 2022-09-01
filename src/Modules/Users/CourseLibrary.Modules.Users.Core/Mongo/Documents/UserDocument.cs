using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Documents;

internal class UserDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDocument(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Password = user.Password;
        Role = user.Role;
        IsActive = user.IsActive;
        CreatedAt = user.CreatedAt;
    }

    public User ToEntity() => new(Id, Email, Password, Role, IsActive, CreatedAt);
}
