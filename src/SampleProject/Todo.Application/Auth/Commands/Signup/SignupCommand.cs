using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Auth;
using Todo.Domain.Entities.Auth;
using Todo.Domain.Errors;

namespace Todo.Application.Auth.Commands.Signup;

public class SignupCommand : Command<SignupRequest, Result<HttpError>>
{
    private readonly ILogger<SignupCommand> _logger;
    private readonly IAuthRepository _authRepository;
    
    public SignupCommand(ILogger<SignupCommand> logger, IAuthRepository authRepository)
    {
        _logger = logger;
        _authRepository = authRepository;
    }

    public override SignupRequest? Input { get; set; }
    public override async Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var signupRequest = Input;
        if (signupRequest is null)
        {
            return Errors.General.NullReferenceError(_logger, nameof(signupRequest));
        }
        
        var user = new User
        {
            Email = signupRequest.Email
        };
        
        var userCreationResult = await _authRepository.CreateUserAsync(user, signupRequest.Password);
        if (userCreationResult.Succeeded == false)
        {
            var validationErrors = userCreationResult.Errors
                .Select(ie => new ValidationInnerError(ie.Code, ie.Description, null))
                .ToList();
            return Errors.General.ValidationError(_logger, validationErrors);
        }

        return Result.Ok();
    }
}