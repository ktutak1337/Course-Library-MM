using CourseLibrary.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Modules.Notifications.Api;

public class NotificationsModule: IModule
{
    public string Name { get; } = "Notifications";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "notifications"
    };

    public void Register(IServiceCollection services, IConfiguration configuration)
    {

    }

    public void Use(IApplicationBuilder app)
    {
    }

    public void Expose(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/notifications", () => "Notifications API!").WithTags("Notifications").WithName("Notifications entry point");
    }
}
