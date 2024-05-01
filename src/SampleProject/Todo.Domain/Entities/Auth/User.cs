using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities.Auth;

/// <summary>
/// Represents a user of our application and also an IdentityUser for auth.
/// For now this class doesn't add anything to the IdentityUser class, but in most cases there will be a business
/// requirement to add more columns to users
/// </summary>
public class User : IdentityUser
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public User() { /* Empty constructor needed for dapper */ }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}