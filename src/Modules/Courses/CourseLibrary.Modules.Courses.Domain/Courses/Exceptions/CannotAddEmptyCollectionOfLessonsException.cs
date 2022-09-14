using CourseLibrary.Shared.Abstractions.Exceptions;
using CourseLibrary.Shared.Abstractions.Kernel.Types;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class CannotAddEmptyCollectionOfLessonsException: CourseLibraryException
{
    public ModuleId ModuleId { get; }

    public CannotAddEmptyCollectionOfLessonsException(ModuleId moduleId) : base(
        $"Cannot add an empty collection of lessons to the module with the ID: {moduleId}")

    {
        ModuleId = moduleId;
    }
}
