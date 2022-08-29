using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Users.Core.Events;

public record SignedUp(Guid UserId, string Email, string Role) : IEvent;
