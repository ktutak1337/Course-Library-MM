using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CourseLibrary.Modules.Courses.Api;

public static class CoursesModule
{
    public static WebApplication UseCoursesApi(this WebApplication app)
    {
        app.MapGet("/courses", () => "Courses API!").WithTags("Courses");

        return app;
    }
}
