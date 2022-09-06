using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Documents;

internal class UserDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleDocument Role { get; set; }
    public UserState State { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDocument(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Password = user.Password;
        Role = new RoleDocument(user.Role);
        State = user.State;
        CreatedAt = user.CreatedAt;
    }

    public User ToEntity() => new(Id, Email, Password, Role.ToEntity(), State, CreatedAt);
}
