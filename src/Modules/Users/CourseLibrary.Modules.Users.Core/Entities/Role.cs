namespace CourseLibrary.Modules.Users.Core.Entities;

internal class Role
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> Permissions { get; set; } = new List<string>();

    public static string Default => User;

    public const string User = "user";
    public const string Admin = "admin";

    public Role(string name, IEnumerable<string> permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}
