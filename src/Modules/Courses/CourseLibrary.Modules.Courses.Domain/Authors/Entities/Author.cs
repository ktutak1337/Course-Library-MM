using CourseLibrary.Modules.Courses.Domain.Authors.Events;
using CourseLibrary.Shared.Abstractions.Domain;
using CourseLibrary.Shared.Abstractions.Kernel.Types;
using CourseLibrary.Shared.Abstractions.Kernel.ValueObjects;

namespace CourseLibrary.Modules.Courses.Domain.Authors.Entities;

public class Author : AggregateRoot
{
    public AuthorId Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Url AvatarUrl { get; private set; }
    public string Notes { get; private set; }

    private Author() { }
    
    private Author(AuthorId id, FullName fullName, Email email, Url avatarUrl, string? notes)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        AvatarUrl = avatarUrl;
        Notes = notes ?? string.Empty;
    }

    public static Author Create(AuthorId id, FullName fullName, Email email, Url avatarUrl, string notes)
    {
        var author = new Author(id, fullName, email, avatarUrl, notes);
        author.AddEvent(new AuthorCreated(author));
        return author;
    }
}
