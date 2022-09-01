using CourseLibrary.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Modules.Users.Core.Commands;

public record SignIn([Required] [EmailAddress] string Email, [Required] string Password) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
