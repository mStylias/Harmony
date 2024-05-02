using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Application.Auth.Commands.Signup;

public class SignupValidator : IHarmonyOperationValidator<SignupCommand, Result<HttpError>>
{
    private readonly ILogger<SignupValidator> _logger;

    public SignupValidator(ILogger<SignupValidator> logger)
    {
        _logger = logger;
    }
    
    public async Task<Result<HttpError>> ValidateAsync(SignupCommand operation, CancellationToken cancellation)
    {
        var validationErrors = new List<ValidationInnerError>();
        var signupRequest = operation.Input;
        
        if (signupRequest is null)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.NullSignupRequest,
                "Signup request was null", null));
            return Errors.General.ValidationError(_logger, validationErrors);
        }
        
        if (signupRequest.Password != signupRequest.ConfirmPassword)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.PasswordsDontMatch,
                "Passwords don't match", "password"));
            return Errors.General.ValidationError(_logger, validationErrors);
        }

        return Result.Ok();
    }
}