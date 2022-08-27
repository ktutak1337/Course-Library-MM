using CourseLibrary.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Modules.Courses.Api;

public class CoursesModule: IModule
{
    public string Name { get; } = "Courses";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "courses"
    };

    public void Register(IServiceCollection services, IConfiguration configuration)
    {

    }

    public void Use(IApplicationBuilder app)
    {
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/courses", () => "Courses API!").WithTags("Courses").WithName("Courses entry point");
    }
}
