using CourseLibrary.Shared.Abstractions;
using CourseLibrary.Shared.Infrastructure;
using CourseLibrary.Shared.Infrastructure.Logging;
using CourseLibrary.Shared.Infrastructure.Modules;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddEndpointsApiExplorer();

builder.Host.ConfigureModules().UseLogging();

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration, "MySpot.Modules.");
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddModularInfrastructure(builder.Configuration, assemblies, modules);

foreach (var module in modules)
{
    module.Register(builder.Services, builder.Configuration);
}

var app = builder.Build();

app.UseModularInfrastructure();
app.UseHttpsRedirection();

foreach (var module in modules)
{
    module.Use(app);
}

app.MapGet("/", (AppInfo appInfo) => appInfo).WithTags("API")
    .WithName("Info")
    .WithMetadata(new SwaggerOperationAttribute(summary: "Returns information about the application."));

app.MapGet("/ping", () => "pong").WithTags("API")
    .WithName("Pong")
    .WithMetadata(new SwaggerOperationAttribute(summary: "A simple health check returns information about the API status."));

app.MapGet("/modules", (ModuleInfoProvider moduleInfoProvider) => moduleInfoProvider.Modules)
    .WithTags("API")
    .WithMetadata(new SwaggerOperationAttribute(summary: "Returns a list of modules."));

foreach (var module in modules)
{
    module.Expose(app);
}

assemblies.Clear();
modules.Clear();

app.Run();
