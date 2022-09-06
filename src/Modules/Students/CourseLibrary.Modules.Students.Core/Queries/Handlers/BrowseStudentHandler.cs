using CourseLibrary.Modules.Students.Core.DTOs;
using CourseLibrary.Modules.Students.Core.Mongo.Documents;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Abstractions.Queries;
using CourseLibrary.Shared.Infrastructure;
using CourseLibrary.Shared.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CourseLibrary.Modules.Students.Core.Queries.Handlers;

internal sealed class BrowseStudentHandler : IQueryHandler<BrowseStudent, Paged<StudentDto>>
{
    private readonly IMongoRepository<StudentDocument, Guid> _studentRepository;

    public BrowseStudentHandler(IMongoRepository<StudentDocument, Guid> studentRepository) 
        => _studentRepository = studentRepository;

    public async Task<Paged<StudentDto>> HandleAsync(BrowseStudent query, CancellationToken cancellationToken = default)
    {
        var students = _studentRepository.Collection
            ?.AsQueryable();

        if (!query.State.IsEmpty())
        {
            var state = query.State.ToLowerInvariant();
            students = state switch
            {
                "active" => students.Where(x => x.IsActive),
                "locked" => students.Where(x => !x.IsActive),
                _ => students
            };
        }
        
        var result = await students
            .OrderByDescending(x => x.CreatedAt)
            .PaginateAsync(query);

        return new Paged<StudentDto>
        {
            CurrentPage = result.CurrentPage,
            TotalPages = result.TotalPages,
            TotalResults = result.TotalResults,
            ResultsPerPage = result.ResultsPerPage,
            Items = result.Items.Select(x => new StudentDto
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                Bio = x.Bio,
                AvatarUrl = x.AvatarUrl,
                IsActive = x.IsActive,
                Notes = x?.Notes,
                State = x.IsActive ? "active" : "locked",
                CreatedAt = x.CreatedAt
            }).ToList()
        };
    }
}
