namespace Todo.Domain.Rules;

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