namespace Harmony.Results.ErrorTypes.InnerErrorTypes;

public record ValidationInnerError
(
    string Code,
    string PropertyName
);