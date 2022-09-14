using CourseLibrary.Shared.Abstractions.Exceptions;
using CourseLibrary.Shared.Abstractions.Kernel.Types;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class ModuleNotFoundException : CourseLibraryException
{
    public ModuleId ModuleId { get; }

    public ModuleNotFoundException(ModuleId moduleId) 
        : base($"Can not find a module with ID: {moduleId}.")
    {
        ModuleId = moduleId;
    }
}
