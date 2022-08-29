using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CourseLibrary.Shared.Abstractions.Modules;
using CourseLibrary.Modules.Users.Core;

namespace CourseLibrary.Modules.Users.Api;

internal sealed class UsersModule : IModule
{
    public string Name { get; } = "Users";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseCore();
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/users", () => "Users API!").WithTags("Users").WithName("Users entry point");
    }
}
