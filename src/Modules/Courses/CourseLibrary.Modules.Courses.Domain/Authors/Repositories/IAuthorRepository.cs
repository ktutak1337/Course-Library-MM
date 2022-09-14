using CourseLibrary.Modules.Courses.Domain.Authors.Entities;

namespace CourseLibrary.Modules.Courses.Domain.Authors.Repositories;

public interface IAuthorRepository
{
    Task<Author> GetAsync(Guid id);
    Task<IEnumerable<Author>> BrowseAsync();
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Author course);
    Task UpdateAsync(Author course);
    Task DeleteAsync(Guid id);
}
