using CourseLibrary.Modules.Users.Core.Mongo;
using CourseLibrary.Modules.Users.Core.Mongo.Documents;
using CourseLibrary.Modules.Users.Core.Mongo.Repositories;
using CourseLibrary.Modules.Users.Core.Repositories;
using CourseLibrary.Modules.Users.Core.Services;
using CourseLibrary.Shared.Infrastructure.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Modules.Users.Core;

public static class Extensions
{
    private const string Schema = "users-module";

    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddMongoRepository<UserDocument, Guid>($"{Schema}.users")
            .AddMongoRepository<RoleDocument, Guid>($"{Schema}.roles");
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        app.UseMongo();

        return app;
    }
}
