using CourseLibrary.Modules.Users.Core.DTOs;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Users.Core.Queries;

internal class GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; set; }
}
