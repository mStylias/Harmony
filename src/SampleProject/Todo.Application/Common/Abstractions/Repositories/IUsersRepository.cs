using Todo.Domain.Entities.Auth;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<User?> GetUserByEmailAsync(string email);
}