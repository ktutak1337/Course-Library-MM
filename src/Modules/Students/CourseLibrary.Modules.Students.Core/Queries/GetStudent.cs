using CourseLibrary.Modules.Students.Core.DTOs;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Students.Core.Queries;

public class GetStudent: IQuery<StudentDetailsDto>
{
    public Guid StudentId { get; set; }
}
