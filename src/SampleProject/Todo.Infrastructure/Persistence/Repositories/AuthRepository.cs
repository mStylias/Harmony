namespace Todo.Infrastructure.Persistence.Repositories;

public class AuthRepository
{
    private readonly DapperDbContext _dbContext;

    public AuthRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}