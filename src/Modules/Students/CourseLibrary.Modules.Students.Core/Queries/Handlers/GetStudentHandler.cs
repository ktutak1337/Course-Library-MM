using CourseLibrary.Modules.Students.Core.DTOs;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Queries;

namespace CourseLibrary.Modules.Students.Core.Queries.Handlers;

internal sealed class GetStudentHandler : IQueryHandler<GetStudent, StudentDetailsDto>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentHandler(IStudentRepository studentRepository) 
        => _studentRepository = studentRepository;

    public async Task<StudentDetailsDto> HandleAsync(GetStudent query, CancellationToken cancellationToken = default)
        => (await _studentRepository.GetAsync(query.StudentId))?.AsDetailsDto();
}
