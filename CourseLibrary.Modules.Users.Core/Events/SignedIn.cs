using CourseLibrary.Shared.Abstractions.Events;

namespace CourseLibrary.Modules.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;
