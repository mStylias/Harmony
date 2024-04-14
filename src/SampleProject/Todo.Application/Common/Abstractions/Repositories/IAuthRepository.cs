using Microsoft.AspNetCore.Identity;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Auth;

namespace Todo.Infrastructure.Persistence.Repositories;

public interface IAuthRepository : IEfCoreRepositoryBase, IDisposable
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
}