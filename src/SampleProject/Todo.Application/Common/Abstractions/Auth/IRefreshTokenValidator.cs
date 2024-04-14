using System.IdentityModel.Tokens.Jwt;
using Harmony.MinimalApis.Errors;
using Harmony.Results;

namespace Todo.Application.Common.Abstractions.Auth;

public interface IRefreshTokenValidator
{
    Result<JwtSecurityToken, HttpError> Validate(string refreshToken);
}