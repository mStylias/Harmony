namespace Todo.Infrastructure.Common.Options.Validators;

public static class JwtOptionsValidator
{
    public static bool Validate(this JwtOptions options)
    {
        var isValid = true;
        
        // Validate key
        var isKeyValid = Guid.TryParse(options.Key, out Guid keyGuid);
        if (isKeyValid == false)
        {
            Console.Error.WriteLine($"The jwt key must be a GUID. The provided value '{options.Key}' is not");
            isValid = false;
        }
        
        // Validate Issuer, Audience, Subject
        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            Console.Error.WriteLine("The Jwt issuer can't be empty");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            Console.Error.WriteLine("The Jwt audience can't be empty");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(options.Subject))
        {
            Console.Error.WriteLine("The Jwt subject can't be empty");
            isValid = false;
        }

        return isValid;
    }
}