namespace Todo.Domain.Errors.Inner;

public static class InnerErrorCodes
{
    public static class Validation
    {
        public const string NullSignupRequest = nameof(NullSignupRequest);
        public const string PasswordsDontMatch = nameof(PasswordsDontMatch);
        public const string RequiredPropertyNotProvided = nameof(RequiredPropertyNotProvided);
        public const string MaximumCharactersExceeded = nameof(MaximumCharactersExceeded);
        public const string EntityDoesNotExist = nameof(EntityDoesNotExist);
        public const string EntityAlreadyExists = nameof(EntityAlreadyExists);
        public const string InvalidEnumValue = nameof(InvalidEnumValue);
    }
}