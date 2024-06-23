using Todo.Domain.Entities.Auth;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<bool> UserExistsAsync(string userId);
    Task<User?> GetUserByEmailAsync(string email);
}