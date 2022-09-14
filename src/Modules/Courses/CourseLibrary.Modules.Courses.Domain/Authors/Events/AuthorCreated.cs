using CourseLibrary.Modules.Courses.Domain.Authors.Entities;
using CourseLibrary.Shared.Abstractions.Domain;

namespace CourseLibrary.Modules.Courses.Domain.Authors.Events;

public record AuthorCreated(Author Author) : IDomainEvent;
