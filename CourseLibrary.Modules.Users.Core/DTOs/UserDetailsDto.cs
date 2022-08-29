namespace CourseLibrary.Modules.Users.Core.DTOs
{
    internal class UserDetailsDto : UserDto
    {
        public IEnumerable<string> Permissions { get; set; }
    }
}
