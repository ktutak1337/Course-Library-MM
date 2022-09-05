using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Documents
{
    internal class RoleDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Permissions { get; set; }

        public RoleDocument(Role role)
        {
            Name = role.Name;
            Permissions = role.Permissions;
        }

        public Role ToEntity() => new(Name, Permissions);
    }
}
