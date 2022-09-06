using CourseLibrary.Modules.Students.Core;
using CourseLibrary.Modules.Students.Core.Commands;
using CourseLibrary.Modules.Students.Core.Queries;
using CourseLibrary.Shared.Abstractions.Dispatchers;
using CourseLibrary.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;

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
        endpoints.MapGet("/students/{id:guid}", async (Guid id, IDispatcher dispatcher) =>
        {
            var student = await dispatcher.QueryAsync(new GetStudent() {StudentId = id});
            return student is null ? Results.NotFound() : Results.Ok(student);
        }).WithTags("Students").WithMetadata(new SwaggerOperationAttribute(summary: "Returns a single student by `ID`."));
        
        endpoints.MapGet("/students", async (string state, IDispatcher dispatcher) =>
        {
            var students = await dispatcher.QueryAsync(new BrowseStudent(){State = state});
            return students is null ? Results.NotFound() : Results.Ok(students);
        }).WithTags("Students").WithMetadata(new SwaggerOperationAttribute(summary: "Returns a list of students."));
        
        endpoints.MapPut("/students/{studentId:guid}/lock", async (Guid studentId, LockStudent command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command with { StudentId = studentId });
            return Results.NoContent();
        }).WithTags("Students").WithMetadata(new SwaggerOperationAttribute(summary: "Locks the student's account."));
        
        endpoints.MapPut("/students/{studentId:guid}/unlock", async (Guid studentId, UnlockStudent command, IDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command with { StudentId = studentId });
            return Results.NoContent();
        }).WithTags("Students").WithMetadata(new SwaggerOperationAttribute(summary: "Unlocks the student's account."));
    }
}
