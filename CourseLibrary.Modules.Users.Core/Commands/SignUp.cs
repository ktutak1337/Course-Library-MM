using CourseLibrary.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Modules.Users.Core.Commands;

internal record SignUp(Guid UserId, [Required] [EmailAddress] string Email, [Required] string Password, string Role = null) : ICommand;
