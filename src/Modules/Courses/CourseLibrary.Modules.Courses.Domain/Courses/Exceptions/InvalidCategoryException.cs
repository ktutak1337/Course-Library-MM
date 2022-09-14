using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class InvalidCategoryException : CourseLibraryException
{
    public string Category { get; }

    public InvalidCategoryException(string category)
        : base($"Category: '{category}' is invalid.")
    {
        Category = category;
    }
}
