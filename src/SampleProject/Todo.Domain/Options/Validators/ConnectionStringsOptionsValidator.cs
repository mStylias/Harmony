namespace Todo.Domain.Options.Validators;

public static class ConnectionStringsOptionsValidator
{
    public static bool Validate(this ConnectionStringsOptions options)
    {
        var isValid = true;
        
        if (string.IsNullOrWhiteSpace(options.Default))
        {
            Console.Error.WriteLine($"The {nameof(options.Default)} connection string is empty");
            isValid = false;
        }

        return isValid;
    } 
}