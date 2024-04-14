namespace Todo.Infrastructure.Common.Options.Validators;

public static class RefreshTokenOptionsValidator
{
    public static bool Validate(this RefreshTokenOptions options)
    {
        var isValid = true;

        // Validate key
        var isKeyValid = Guid.TryParse(options.Key, out Guid keyGuid);
        if (isKeyValid == false)
        {
            Console.Error.WriteLine($"The refresh token key must be a GUID. The provided value '{options.Key}' is not");
            isValid = false;
        }
        
        if (options.ExpirationTime <= TimeSpan.Zero)
        {
            Console.Error.WriteLine("The refresh token expiration time can't be less than or equal to zero");
            isValid = false;
        }

        return isValid;
    }
}