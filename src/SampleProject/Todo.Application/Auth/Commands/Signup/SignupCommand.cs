using Harmony.Cqrs;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Application.Auth.Common;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Auth;
using Todo.Domain.Entities.Auth;
using Todo.Domain.Errors;
using Todo.Domain.Successes;

namespace Todo.Application.Auth.Commands.Signup;

public class SignupCommand : Command<SignupRequest, Result<AuthTokensModel, HttpError>>
{
    private readonly ILogger<SignupCommand> _logger;
    private readonly IAuthRepository _authRepository;
    private readonly ITokenCreationService _tokenCreationService;
    private readonly IOperationValidator<SignupCommand, Result<HttpError>> _validator;

    public SignupCommand(ILogger<SignupCommand> logger, IAuthRepository authRepository, 
        ITokenCreationService tokenCreationService, 
        IOperationValidator<SignupCommand, Result<HttpError>> validator)
    {
        _logger = logger;
        _authRepository = authRepository;
        _tokenCreationService = tokenCreationService;
        _validator = validator;
    }

    public override SignupRequest? Input { get; set; }
    public override async Task<Result<AuthTokensModel, HttpError>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }
        
        // At this point signup request is guaranteed to have a value, because it is checked in the validator
        var signupRequest = Input!;
        
        var user = new User
        {
            UserName = signupRequest.Email,
            Email = signupRequest.Email
        };
        
        IdentityResult userCreationResult = await _authRepository.CreateUserAsync(user, signupRequest.Password);
        if (userCreationResult.Succeeded == false)
        {
            var validationErrors = userCreationResult.Errors
                .Select(ie => new ValidationInnerError(ie.Code, ie.Description, GetErrorPropertyName(ie.Code)))
                .ToList();
            
            return Errors.General.ValidationError(_logger, validationErrors);
        }

        var tokens = _tokenCreationService.GenerateTokens(user.Id);

        await _authRepository.AddNewUserRefreshToken(user.Id, tokens.RefreshToken);

        var signupSuccess = Successes.Auth.SignupSuccess(_logger, user.Email);

        return Result<AuthTokensModel, HttpError>.Ok(tokens, signupSuccess);
    }

    private static string? GetErrorPropertyName(string code)
    {
        if (code.Contains("Password"))
        {
            return "password";
        }
        
        if (code.Contains("Email"))
        {
            return "email";
        }

        return null;
    }
}