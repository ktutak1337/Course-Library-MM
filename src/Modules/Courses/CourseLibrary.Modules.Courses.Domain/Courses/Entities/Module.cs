using CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;
using CourseLibrary.Shared.Abstractions.Kernel.Types;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Entities;

public class Module
{
    private HashSet<Lesson> _lessons = new();
    
    public ModuleId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IEnumerable<Lesson> Lessons => _lessons;

    private Module() { }
    
    public Module(ModuleId id, string name, string description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    private void AddLesson(Lesson lesson)
    {
        if (lesson is null)
        {
            throw new CannotAddEmptyLessonException(Id, lesson.Id);
        }

        _lessons.Add(lesson);
    }

    private void AddManyLessons(IEnumerable<Lesson> lessons)
    {
        if (lessons is null || !lessons.Any())
        {
            throw new CannotAddEmptyCollectionOfLessonsException(Id);
        }

        foreach (var lesson in lessons)
        {
            AddLesson(lesson);
        }
    }
}
