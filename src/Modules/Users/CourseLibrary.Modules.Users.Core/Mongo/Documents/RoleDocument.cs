using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Documents
{
    internal class RoleDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public IEnumerable<UserDocument> Users { get; set; }

        public RoleDocument(Role role)
        {
            Name = role.Name;
            Permissions = role.Permissions;
            Users = role.Users?.Select(user => new UserDocument(user));
        }

        public Role ToEntity() => new(Name, Permissions, Users.Select(user =>
            new User(user.Id, user.Email, user.Password, user.Role, user.IsActive, user.CreatedAt)));
    }
}
