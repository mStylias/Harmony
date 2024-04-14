using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Todo.Domain.Entities.Auth;

namespace Todo.Infrastructure.Persistence;

public class AuthDbContext : IdentityDbContext<User, IdentityRole, string>
{
    private readonly ILoggerFactory? _loggerFactory;

    public AuthDbContext(DbContextOptions options, ILoggerFactory? loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_loggerFactory is not null)
        {
            // Setting logger factory like this is not necessary, but helps in unit testing if you want to 
            // see the entity framework logs
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}