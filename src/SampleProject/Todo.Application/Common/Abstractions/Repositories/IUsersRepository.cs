using Todo.Domain.Entities.Auth;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email);
}