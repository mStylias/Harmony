using Harmony.Results.Enums;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes.InnerErrorTypes;

public class ValidationInnerError : LoggableHarmonyErrorCore<ValidationInnerError>
{
    public ValidationInnerError(
        string code, 
        string? propertyName = null,
        string? description = null,
        Severity severity = Severity.Error) 
        : base(severity)
    {
        this.Code = code;
        this.Description = description;
        this.PropertyName = propertyName;
    }
    
    public string Code { get; set; }
    
    public string? Description { get; set; }
    
    public string? PropertyName { get; set; }
}