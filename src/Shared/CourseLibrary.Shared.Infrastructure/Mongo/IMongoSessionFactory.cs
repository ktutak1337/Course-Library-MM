using MongoDB.Driver;

namespace CourseLibrary.Shared.Infrastructure.Mongo;

public interface IMongoSessionFactory
{
    Task<IClientSessionHandle> CreateAsync();
}
