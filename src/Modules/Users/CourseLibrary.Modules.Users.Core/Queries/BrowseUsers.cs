using CourseLibrary.Modules.Users.Core.DTOs;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Users.Core.Queries;

internal class BrowseUsers : PagedQuery<UserDto>
{
    public string Email { get; set; }
    public string Role { get; set; }
    public string State { get; set; }
}
