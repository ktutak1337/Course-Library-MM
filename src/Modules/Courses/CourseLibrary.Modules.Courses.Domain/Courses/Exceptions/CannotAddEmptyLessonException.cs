using CourseLibrary.Shared.Abstractions.Exceptions;
using CourseLibrary.Shared.Abstractions.Kernel.Types;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class CannotAddEmptyLessonException : CourseLibraryException
{
    public ModuleId ModuleId { get; }
    public LessonId LessonId { get; }

    public CannotAddEmptyLessonException(ModuleId moduleId, LessonId lessonId) : base(
        $"Cannot add an empty lesson with ID: {lessonId} to the module with the ID: {moduleId}")

    {
        ModuleId = moduleId;
        LessonId = lessonId;
    }
}
