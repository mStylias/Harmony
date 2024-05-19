using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Todo.Application.Auth.Common;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Auth;
using Todo.Domain.Errors;
using Todo.Domain.Successes;

namespace Todo.Application.Auth.Queries.Login;

public class LoginQuery : Query<LoginRequest, Result<AuthTokensModel, HttpError>>
{
    private readonly ILogger<LoginQuery> _logger;
    private readonly ITokenCreationService _tokenCreationService;
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthRepository _authRepository;

    public LoginQuery(ILogger<LoginQuery> logger, ITokenCreationService tokenCreationService,
        IUsersRepository usersRepository, IAuthRepository authRepository)
    {
        _logger = logger;
        _tokenCreationService = tokenCreationService;
        _usersRepository = usersRepository;
        _authRepository = authRepository;
    }
    
    public override LoginRequest? Input { get; set; }

    public override async Task<Result<AuthTokensModel, HttpError>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var loginRequest = Input;
        if (loginRequest is null)
        {
            return Errors.General.NullReferenceError(_logger, nameof(loginRequest));
        }

        var user = await _usersRepository.GetUserByEmailAsync(loginRequest.Email);
        if (user is null)
        {
            return Errors.Auth.InvalidCredentials(_logger, loginRequest.Email);
        }
        
        var isPasswordCorrect = await _authRepository.CheckPasswordAsync(user, loginRequest.Password);
        if (isPasswordCorrect == false)
        {
            return Errors.Auth.InvalidCredentials(_logger, loginRequest.Email);
        }

        var tokens = _tokenCreationService.GenerateTokens(user.Id);
        
        await _authRepository.UpdateRefreshToken(tokens.RefreshToken, user.Id);

        Successes.Auth.LoginSuccess(_logger, loginRequest.Email).Log();
        
        return tokens;
    }
}