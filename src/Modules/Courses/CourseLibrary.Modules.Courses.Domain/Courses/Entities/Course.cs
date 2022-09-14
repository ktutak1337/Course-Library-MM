using CourseLibrary.Modules.Courses.Domain.Courses.Exceptions;
using CourseLibrary.Modules.Courses.Domain.Courses.ValueObjects;
using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Kernel.Types;

namespace CourseLibrary.Modules.Courses.Domain.Courses.Entities;

public class Course : AggregateRoot
{
    private HashSet<Module> _modules = new();
    private HashSet<AuthorId> _authors = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public Language Language { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    public IEnumerable<Module> Modules => _modules;
    public IEnumerable<AuthorId> Authors => _authors;
    
    public Course(string name, string description, Category category, Difficulty difficulty, Language language, 
        DateTime createdAt, DateTime? updatedAt = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidCourseNameException(Id, name);
        }
        
        Name = name;

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new InvalidCourseDescriptionException(Id, name);
        }
        
        Description = description;
        Category = category;
        Difficulty = difficulty;
        Language = language;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        
        IncrementVersion();
    }

    public void AddModule(Module module)
    {
        if (module is null)
        {
            throw new CannotAddEmptyModuleException(Id);
        }
        
        _modules.Add(module);
        IncrementVersion();
    }
    
    public Module RemoveModule(ModuleId moduleId)
    {
        var module = GetModule(moduleId);
        
        _modules.Remove(module);
        IncrementVersion();

        return module;
    }
    
    private Module GetModule(ModuleId moduleId)
    {
        var module = _modules.SingleOrDefault(module => module.Id == moduleId);

        if (module is null)
        {
            throw new ModuleNotFoundException(moduleId);
        }

        return module;
    }
}
