using Microsoft.AspNetCore.Identity;
using Todo.Domain.Entities.Auth;
using Todo.Infrastructure.Persistence.Repositories.Base;

namespace Todo.Infrastructure.Persistence.Repositories;

public class AuthRepository : EfCoreRepositoryBase, IAuthRepository
{
    private readonly AuthDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public AuthRepository(AuthDbContext dbContext, UserManager<User> userManager) : base(dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public void Dispose()
    {
        _userManager.Dispose();
        GC.SuppressFinalize(this);
    }
}