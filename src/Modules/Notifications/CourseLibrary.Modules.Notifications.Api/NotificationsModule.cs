using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CourseLibrary.Modules.Notifications.Api;

public static class NotificationsModule
{
    public static WebApplication UseNotificationssApi(this WebApplication app)
    {
        app.MapGet("/notifications", () => "Notifications API!").WithTags("Notifications");

        return app;
    }
}
