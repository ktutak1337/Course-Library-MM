using CourseLibrary.Modules.Users.Core.Entities;

namespace CourseLibrary.Modules.Users.Core.Repositories;

internal interface IRoleRepository
{
    Task<Role> GetAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
