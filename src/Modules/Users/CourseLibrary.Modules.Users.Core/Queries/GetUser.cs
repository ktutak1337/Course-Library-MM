using CourseLibrary.Modules.Users.Core.DTOs;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Users.Core.Queries;

public class GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; set; }
}
