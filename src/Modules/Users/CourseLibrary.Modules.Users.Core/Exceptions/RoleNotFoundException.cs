using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Users.Core.Exceptions;

internal class RoleNotFoundException : CourseLibraryException
{
    public RoleNotFoundException(string role) 
        : base($"Role: '{role}' was not found.") { }
}
