using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using Todo.Application.Auth.Common;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Successes;

namespace Todo.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : Command<RefreshRequest, Result<AuthTokensModel, HttpError>>
{
    private readonly ILogger<RefreshTokenCommand> _logger;
    private readonly ITokenCreationService _tokenCreationService;
    private readonly IAuthRepository _authRepository;
    private readonly IRefreshTokenValidator _refreshTokenValidator;

    public RefreshTokenCommand(
        ILogger<RefreshTokenCommand> logger,
        ITokenCreationService tokenCreationService,
        IAuthRepository authRepository,
        IRefreshTokenValidator refreshTokenValidator)
    {
        _logger = logger;
        _tokenCreationService = tokenCreationService;
        _authRepository = authRepository;
        _refreshTokenValidator = refreshTokenValidator;
    }
    public override RefreshRequest? Input { get; set; }

    public override async Task<Result<AuthTokensModel, HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var refreshRequest = Input;
        if (refreshRequest is null)
        {
            return Errors.General.NullReferenceError(_logger, nameof(refreshRequest));
        }
        
        var validationResult = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }
                
        var userIdResult = await _authRepository.GetUserIdByRefreshToken(refreshRequest.RefreshToken);
        if (userIdResult.IsError)
        {
            return userIdResult.Error;
        }

        var securityToken = validationResult.Value!;
        var existingUserId = userIdResult.Value!;
        var refreshTokenExpiration = securityToken.ValidTo;
                
        // Create new access token and refresh token for the user
        var authTokensModel = _tokenCreationService.GenerateTokens(existingUserId, refreshTokenExpiration);

        // Override the previous refresh token with the new one, while keeping the same user id
        await _authRepository.UpdateRefreshToken(authTokensModel.RefreshToken, existingUserId);

        Successes.Auth.RefreshTokenSuccess(_logger, existingUserId).Log();

        return authTokensModel;
    }
}