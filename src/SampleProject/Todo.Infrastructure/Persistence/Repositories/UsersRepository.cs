using Dapper;
using Microsoft.AspNetCore.Identity;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Auth;

namespace Todo.Infrastructure.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DapperDbContext _dapperContext;
    private readonly UserManager<User> _userManager;

    public UsersRepository(DapperDbContext dapperContext, UserManager<User> userManager)
    {
        _dapperContext = dapperContext;
        _userManager = userManager;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        using var connection = _dapperContext.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<int>(
            "SELECT 1 FROM AspNetUsers WHERE Id = @UserId", new { UserId = userId });

        return result == 1;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user;
    }
}