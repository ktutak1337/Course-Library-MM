using CourseLibrary.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Modules.Users.Core.Commands;

public record SignUp(Guid UserId, [Required] [EmailAddress] string Email, [Required] string Password, string Role = null) : ICommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; }
}
