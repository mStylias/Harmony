namespace Todo.Domain.Rules;

/// <summary>
/// Represents the domain rules of our application. Like Errors this class is segmented in different partial classes,
/// one for each logical group. E.g. Rules.Auth, Rules.Todos, etc.
/// </summary>
public static partial class Rules
{
    public static class Auth
    {
        public const bool RequireConfirmedAccount = false;
        public const bool RequireUniqueEmail = true;
        public const bool RequireDigit = true;
        public const int RequiredLength = 6;
        public const bool RequireNonAlphanumeric = false;
        public const bool RequireUppercase = true;
        public const bool RequireLowercase = true;
    }
}