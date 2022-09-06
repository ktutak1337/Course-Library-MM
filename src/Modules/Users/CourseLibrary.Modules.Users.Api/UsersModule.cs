using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CourseLibrary.Shared.Abstractions.Modules;
using CourseLibrary.Modules.Users.Core;
using CourseLibrary.Shared.Abstractions.Dispatchers;
using CourseLibrary.Modules.Users.Core.Queries;
using CourseLibrary.Modules.Users.Core.Commands;
using CourseLibrary.Modules.Users.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

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
        endpoints.MapGet("/users/{id:guid}", async (Guid id, IDispatcher dispatcher) =>
        {
            var user = await dispatcher.QueryAsync(new GetUser {UserId = id});
            return user is null ? Results.NotFound() : Results.Ok(user);
        }).WithTags("Users").WithMetadata(new SwaggerOperationAttribute(summary: "Returns a single user by `ID`."));

        endpoints.MapGet("/me", async (IDispatcher dispatcher, HttpContext context) =>
        {
            var user = await dispatcher.QueryAsync(new GetUser {UserId = UserId(context)});
            return user is null ? Results.NotFound() : Results.Ok(user);
        }).RequireAuthorization().WithTags("Account").WithMetadata(new SwaggerOperationAttribute(summary: "Returns information about the currently logged-in user."));

        endpoints.MapPost("/sign-up", async (SignUp command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command with {UserId = Guid.NewGuid()});
            return Results.NoContent();
        }).WithTags("Account").WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new user account."));

        endpoints.MapPost("/sign-in", async (SignIn command, IDispatcher dispatcher, ITokenStorage storage) =>
            {
                await dispatcher.SendAsync(command);
                var jwt = storage.Get();
                return Results.Ok(jwt);
            }).WithTags("Account").WithMetadata(new SwaggerOperationAttribute(summary: "It allows you to sign in to an account."));

        static Guid UserId(HttpContext context)
            => string.IsNullOrWhiteSpace(context.User.Identity?.Name) ? Guid.Empty : Guid.Parse(context.User.Identity.Name);
    }
}
