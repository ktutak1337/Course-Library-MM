using CourseLibrary.Modules.Users.Core.DTOs;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository) 
        => _userRepository = userRepository;

    public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
        => (await _userRepository.GetAsync(query.UserId))?.AsDetailsDto();
}
