namespace CourseLibrary.Modules.Users.Core.Entities;

internal class Role
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> Permissions { get; set; } = new List<string>();
    public IEnumerable<User> Users { get; set; } = new List<User>();

    public static string Default => User;

    public const string User = "user";
    public const string Admin = "admin";

    public Role(string name, IEnumerable<string> permissions, IEnumerable<User> users)
    {
        Name = name;
        Permissions = permissions;
        Users = users;
    }
}
