using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Modules.Users.Core.Mongo.Documents;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Repositories;

internal class RoleRepository : IRoleRepository
{

    private readonly IMongoRepository<RoleDocument, Guid> _repository;

    public RoleRepository(IMongoRepository<RoleDocument, Guid> repository)
        => _repository = repository;

    public async Task<Role> GetAsync(string name)
        => (await _repository.GetAsync(x => x.Name == name))?.ToEntity();

    public async Task<IEnumerable<Role>> GetAllAsync()
        => (await _repository.FindAsync(_ => true))
        ?.Select(documnet => documnet?.ToEntity());

    public async Task AddAsync(Role role)
        => await _repository.AddAsync(new RoleDocument(role));
}
