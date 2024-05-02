namespace Todo.Domain.Errors.Inner;

public static class InnerErrorCodes
{
    public static class Validation
    {
        public const string NullSignupRequest = nameof(NullSignupRequest);
        public const string PasswordsDontMatch = nameof(PasswordsDontMatch);
    }
}