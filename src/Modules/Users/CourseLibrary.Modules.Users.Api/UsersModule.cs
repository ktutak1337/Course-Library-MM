using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CourseLibrary.Shared.Abstractions.Modules;

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

    }

    public void Use(IApplicationBuilder app)
    {
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/users", () => "Users API!").WithTags("Users").WithName("Users entry point");
    }
}
