using CourseLibrary.Modules.Courses.Domain.Courses.Entities;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Repositories;

public interface ICourseRepository
{
    Task<Course> GetAsync(Guid id);
    Task<IEnumerable<Course>> BrowseAsync();
    Task<bool> ExistsAsync(Guid id);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid id);
}
