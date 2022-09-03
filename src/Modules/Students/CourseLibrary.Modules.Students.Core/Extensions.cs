using CourseLibrary.Modules.Students.Core.Mongo.Documents;
using CourseLibrary.Modules.Students.Core.Mongo.Repositories;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Infrastructure.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Modules.Students.Core;

public static class Extensions
{
    private const string Schema = "students-module";

    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IStudentRepository, StudentRepository>()
            .AddMongoRepository<StudentDocument, Guid>($"{Schema}.students");
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        return app;
    }
}
