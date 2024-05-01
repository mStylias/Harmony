using Microsoft.AspNetCore.Identity;
using Todo.Application.Common.Abstractions.Repositories.Base;
using Todo.Domain.Entities.Auth;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface IAuthRepository : IEfCoreRepositoryBase, IDisposable
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
}