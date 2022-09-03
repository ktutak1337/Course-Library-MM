using CourseLibrary.Modules.Students.Core.Entities;
using CourseLibrary.Modules.Students.Core.Mongo.Documents;
using CourseLibrary.Modules.Students.Core.Repositories;
using CourseLibrary.Shared.Infrastructure.Mongo;

namespace CourseLibrary.Modules.Students.Core.Mongo.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IMongoRepository<StudentDocument, Guid> _repository;

    public StudentRepository(IMongoRepository<StudentDocument, Guid> repository)
        => _repository = repository;

    public async Task<Student> GetAsync(Guid id)
        => (await _repository.GetAsync(id))?.ToEntity();

    public async Task<Student> GetAsync(string email)
        => (await _repository.GetAsync(document => document.Email == email))?.ToEntity();

    public async Task<IEnumerable<Student>> BrowseAsync()
        => (await _repository.FindAsync(_ => true))
            ?.Select(documnet => documnet.ToEntity());
    public async Task AddAsync(Student student)
        => await _repository.AddAsync(new StudentDocument(student));

    public async Task UpdateAsync(Student student)
        => await _repository.UpdateAsync(new StudentDocument(student));

    public async Task<bool> ExistsAsync(Guid id)
        => await _repository.ExistsAsync(document => document.Id == id);
}
