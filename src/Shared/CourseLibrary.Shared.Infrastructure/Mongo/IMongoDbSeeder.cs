using MongoDB.Driver;

namespace CourseLibrary.Shared.Infrastructure.Mongo;

public interface IMongoDbSeeder
{
    Task SeedAsync(IMongoDatabase database);
}
