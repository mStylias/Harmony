using Harmony.Cqrs.Operations;
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

public class LoginQuery : Query<Result<AuthTokensModel, HttpError>>
{
    private readonly ILogger<LoginQuery> _logger;
    private readonly ITokenCreationService _tokenCreationService;
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthRepository _authRepository;

    public LoginQuery(
        ILogger<LoginQuery> logger, 
        ITokenCreationService tokenCreationService,
        IUsersRepository usersRepository, 
        IAuthRepository authRepository)
    {
        _logger = logger;
        _tokenCreationService = tokenCreationService;
        _usersRepository = usersRepository;
        _authRepository = authRepository;
    }

    public LoginRequest LoginRequest { get; set; } = null!;

    public override async Task<Result<AuthTokensModel, HttpError>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var user = await _usersRepository.GetUserByEmailAsync(LoginRequest.Email).ConfigureAwait(false);
        if (user is null)
        {
            return DomainErrors.Auth.InvalidCredentials(_logger, LoginRequest.Email);
        }
        
        var isPasswordCorrect = await _authRepository.CheckPasswordAsync(user, LoginRequest.Password).ConfigureAwait(false);
        if (isPasswordCorrect == false)
        {
            return DomainErrors.Auth.InvalidCredentials(_logger, LoginRequest.Email);
        }

        var tokens = _tokenCreationService.GenerateTokens(user.Id);
        
        await _authRepository.UpdateRefreshToken(tokens.RefreshToken, user.Id).ConfigureAwait(false);

        Successes.Auth.LoginSuccess(_logger, LoginRequest.Email).Log();
        
        return tokens;
    }
}