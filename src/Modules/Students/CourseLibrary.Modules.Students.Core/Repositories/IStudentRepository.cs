using CourseLibrary.Modules.Students.Core.Entities;

namespace CourseLibrary.Modules.Students.Core.Repositories;

public interface IStudentRepository
{
        Task<Student> GetAsync(Guid id);
        Task<Student> GetAsync(string email);
        Task<IEnumerable<Student>> BrowseAsync();
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task<bool> ExistsAsync(Guid id);
}
