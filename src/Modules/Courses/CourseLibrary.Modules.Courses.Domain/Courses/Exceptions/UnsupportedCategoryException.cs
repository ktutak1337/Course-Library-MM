using CourseLibrary.Shared.Abstractions.Exceptions;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;

internal class UnsupportedCategoryException : CourseLibraryException
{
    public string Category { get; }

    public UnsupportedCategoryException(string category)
        : base($"Category: '{category}' is unsupported.")
    {
        Category = category;
    }
}
