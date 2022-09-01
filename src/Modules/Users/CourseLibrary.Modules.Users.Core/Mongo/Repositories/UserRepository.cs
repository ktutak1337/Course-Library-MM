using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Modules.Users.Core.Mongo.Documents;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Users.Core.Mongo.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;

    public UserRepository(IMongoRepository<UserDocument, Guid> repository)
        => _repository = repository;

    public async Task<User> GetAsync(Guid id)
        => (await _repository.GetAsync(id))?.ToEntity();

    public async Task<User> GetAsync(string email)
        => (await _repository.GetAsync(user => user.Email == email))?.ToEntity();

    public async Task AddAsync(User user)
        => await _repository.AddAsync(new UserDocument(user));

    public async Task UpdateAsync(User user)
        => await _repository.UpdateAsync(new UserDocument(user));
}
