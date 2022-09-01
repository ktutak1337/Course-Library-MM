using CourseLibrary.Shared.Abstractions.Auth;

namespace CourseLibrary.Modules.Users.Core.Services;

public interface ITokenStorage
{
    void Set(JsonWebToken jwt);
    JsonWebToken Get();
}
