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

        return builder;
    }
}
