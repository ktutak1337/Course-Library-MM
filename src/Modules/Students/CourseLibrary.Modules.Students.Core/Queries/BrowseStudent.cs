using CourseLibrary.Modules.Students.Core.DTOs;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Students.Core.Queries;

public class BrowseStudent : PagedQuery<StudentDto>
{
    public string? State { get; set; }
}
