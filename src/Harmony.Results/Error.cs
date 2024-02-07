using System.Text;
using Harmony.Results.Abstractions;
using Microsoft.Extensions.Logging;

namespace Harmony.Results;

public class Error : IError
{
    public ErrorType Type { get; }
    public int HttpStatusCode { get; }
    public List<InnerError>? InnerErrors { get; }
    
    private readonly Action<ILogger>? _logAction;
    private readonly ILogger? _logger;
    
    public Error(ErrorType type, string code, string message, ILogger logger,
        Action<ILogger> addLogging, int httpStatusCode)
    {
        Type = type;
        HttpStatusCode = httpStatusCode;
        InnerErrors = [new(code, null, message)];
        _logger = logger;
        _logAction = addLogging;
    }
    
    public Error(ErrorType type, List<InnerError> innerErrors, int httpStatusCode)
    {
        Type = type;
        HttpStatusCode = httpStatusCode;
        InnerErrors = innerErrors;
    }
    
    public Error(ErrorType type, List<InnerError> innerErrors,
        Action<ILogger> logAction, ILogger logger, int httpStatusCode)
    {
        Type = type;
        HttpStatusCode = httpStatusCode;
        InnerErrors = innerErrors;
        _logAction = logAction;
        _logger = logger;
    }

    public void Log()
    {
        _logAction?.Invoke(_logger!);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine("{");
        
        builder.Append("  \"type\": ");
        builder.Append('"');
        builder.Append(Type.ToString());
        builder.AppendLine("\",");
        
        builder.Append("  \"HttpStatusCode\": ");
        builder.Append(HttpStatusCode);
        builder.AppendLine(",");
        
        builder.AppendLine("  \"InnerErrors\": [");
        
        for (int i = 0; i < InnerErrors?.Count; i++)
        {
            var innerError = InnerErrors[i];
            
            builder.AppendLine("    {");
            
            builder.Append("      \"code\": ");
            builder.Append('"');
            builder.Append(innerError.Code);
            builder.AppendLine("\",");
            
            builder.Append("      \"propertyName\": ");
            builder.Append('"');
            builder.Append(innerError.PropertyName ?? "null");
            builder.AppendLine("\",");
            
            builder.Append("      \"description\": ");
            builder.Append('"');
            builder.Append(innerError.Description);
            builder.AppendLine("\"");

            builder.Append("    }");

            if (i == InnerErrors.Count - 1) continue;
            
            builder.AppendLine(",");
        }

        builder.AppendLine();
        builder.AppendLine("  ]");
        builder.AppendLine("}");
        
        return builder.ToString();
    }
}