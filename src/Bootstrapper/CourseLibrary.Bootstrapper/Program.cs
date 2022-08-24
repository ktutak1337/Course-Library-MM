using CourseLibrary.Modules.Courses.Api;
using CourseLibrary.Modules.Notifications.Api;
using CourseLibrary.Modules.Users.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseUsersApi();
app.UseCoursesApi();
app.UseNotificationssApi();

app.MapGet("/", () => "Course Library API!");

app.Run();
