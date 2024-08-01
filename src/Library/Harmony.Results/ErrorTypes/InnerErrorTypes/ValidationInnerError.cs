using Harmony.Results.Enums;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes.InnerErrorTypes;

public class ValidationInnerError : LoggableHarmonyErrorImpl<ValidationInnerError>
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string? PropertyName { get; set; }

    public ValidationInnerError(string code, string description, string? propertyName, 
        Severity severity = Severity.Error) : base(severity)
    {
        Code = code;
        Description = description;
        PropertyName = propertyName;
    }
    
    public ValidationInnerError(string code, string description, Severity severity = Severity.Error) : base(severity)
    {
        Code = code;
        Description = description;
    }
}