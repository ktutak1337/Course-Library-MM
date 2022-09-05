using CourseLibrary.Modules.Users.Core.Entities;
using CourseLibrary.Modules.Users.Core.Mongo.Documents;
using CourseLibrary.Shared.Infrastructure.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CourseLibrary.Modules.Users.Core.Mongo;

internal static class Extensions
{
    internal static IApplicationBuilder UseMongo(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var users = scope.ServiceProvider.GetRequiredService<IMongoRepository<UserDocument, Guid>>().Collection;
        var userBuilder = Builders<UserDocument>.IndexKeys;

        Task.Run(async () => await users.Indexes.CreateManyAsync(
            new[]
            {
                    new CreateIndexModel<UserDocument>(userBuilder.Ascending(i => i.Email),
                        new CreateIndexOptions
                        {
                            Unique = true
                        })
            }));

        
        var roles = scope.ServiceProvider.GetRequiredService<IMongoRepository<RoleDocument, Guid>>().Collection;
        
        var roleDocuments = new List<RoleDocument>
        {
            new RoleDocument(new Role("user", new List<string> { "users", "students", "courses", "notifications" } )),
            new RoleDocument(new Role("admin", new List<string> { "users", "students", "courses", "notifications" } ))
        };
        
        Task.Run(async () =>
        {
            if (await roles.CountDocumentsAsync(FilterDefinition<RoleDocument>.Empty) == 0)
            {
                await roles.InsertManyAsync(roleDocuments);
            }
        });
        
        return builder;
    }
}
