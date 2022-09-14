using CourseLibrary.Shared.Abstractions.Kernel.Types;
using CourseLibrary.Shared.Abstractions.Kernel.ValueObjects;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Entities;

public class Lesson
{
    private HashSet<string> _attachments = new();

    public LessonId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Url VideoUrl { get; private set; }
    public Url ThumbnailUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Lesson(LessonId id, string name, string description, Url videoUrl, Url thumbnailUrl, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        ThumbnailUrl = thumbnailUrl;
        CreatedAt = createdAt;
    }
}
