using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CourseLibrary.Modules.Users.Api;

public static class UsersModule
{
    public static WebApplication UseUsersApi(this WebApplication app)
    {
        app.MapGet("/users", () => "Users API!").WithTags("Users");

        return app;
    }
}
