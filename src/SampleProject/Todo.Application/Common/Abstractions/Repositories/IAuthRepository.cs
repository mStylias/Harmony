using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.AspNetCore.Identity;
using Todo.Application.Common.Abstractions.Repositories.Base;
using Todo.Domain.Entities.Auth;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface IAuthRepository : IEfCoreRepositoryBase, IDisposable
{
    Task<Result<string, HttpError>> GetUserIdByRefreshToken(string refreshToken,
        CancellationToken cancellationToken = default);
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task AddNewUserRefreshToken(string newUserId, string refreshToken);
    Task UpdateRefreshToken(string newRefreshToken, string userId);
    Task EmptyRefreshToken(string refreshToken);
}