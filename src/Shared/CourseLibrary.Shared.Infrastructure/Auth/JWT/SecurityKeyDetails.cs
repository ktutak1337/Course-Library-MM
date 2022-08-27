using Microsoft.IdentityModel.Tokens;

namespace CourseLibrary.Shared.Infrastructure.Auth.JWT;

internal sealed record SecurityKeyDetails(SecurityKey Key, string Algorithm);
