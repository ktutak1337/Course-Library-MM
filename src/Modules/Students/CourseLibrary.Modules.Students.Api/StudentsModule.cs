using CourseLibrary.Modules.Students.Core;
using CourseLibrary.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Modules.Students.Api;

internal class StudentsModule : IModule
{
    public string Name { get; } = "Students";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "students"
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
        endpoints.MapGet("/students", () => "Students API!").WithTags("Students").WithName("Students entry point");
    }
}
