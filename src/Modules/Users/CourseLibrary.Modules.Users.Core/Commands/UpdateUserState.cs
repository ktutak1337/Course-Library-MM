using CourseLibrary.Shared.Abstractions.Commands;

namespace CourseLibrary.Modules.Users.Core.Commands;

public record UpdateUserState(Guid UserId, string State) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
