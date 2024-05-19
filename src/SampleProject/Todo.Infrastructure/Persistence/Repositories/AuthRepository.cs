using Dapper;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Auth;
using Todo.Domain.Errors;
using Todo.Infrastructure.Persistence.Repositories.Base;

namespace Todo.Infrastructure.Persistence.Repositories;

public class AuthRepository : EfCoreRepositoryBase, IAuthRepository
{
    private readonly ILogger<AuthRepository> _logger;
    private readonly UserManager<User> _userManager;
    private readonly DapperDbContext _dapperContext;

    public AuthRepository(ILogger<AuthRepository> logger, AuthDbContext dbContext, UserManager<User> userManager, 
        DapperDbContext dapperContext) : base(dbContext)
    {
        _logger = logger;
        _userManager = userManager;
        _dapperContext = dapperContext;
    }
    
    public Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    /// <summary>
    /// Gets the refresh token info like user id and username for the given refresh token.
    /// </summary>
    public async Task<Result<string, HttpError>> GetUserIdByRefreshToken(string refreshToken, 
        CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        
        const string sql = @"SELECT user_id FROM refresh_tokens WHERE refresh_token = @refreshToken";
        var userId = await connection.QueryFirstOrDefaultAsync<string>(new CommandDefinition(
            sql, parameters: new { refreshToken }, cancellationToken: cancellationToken));

        if (userId is null)
        {
            return Errors.Auth.InvalidRefreshToken(_logger, refreshToken);
        }
        
        return userId;
    }
    
    public async Task AddNewUserRefreshToken(string newUserId, string refreshToken)
    {
        using var connection = _dapperContext.CreateConnection(); 
        
        const string insertSql = @"INSERT INTO refresh_tokens (user_id, refresh_token) 
                             VALUES (@UserId, @RefreshToken)";
        await connection.ExecuteAsync(insertSql, new { UserId = newUserId, RefreshToken = refreshToken });
    }
    
    public async Task UpdateRefreshToken(string newRefreshToken, string userId)
    {
        using var connection = _dapperContext.CreateConnection(); 
        
        const string updateSql = @"UPDATE refresh_tokens SET refresh_token = @RefreshToken WHERE user_id = @UserId";
        await connection.ExecuteAsync(updateSql, new { RefreshToken = newRefreshToken, UserId = userId });
    }
    
    public void Dispose()
    {
        _userManager.Dispose();
        GC.SuppressFinalize(this);
    }
}