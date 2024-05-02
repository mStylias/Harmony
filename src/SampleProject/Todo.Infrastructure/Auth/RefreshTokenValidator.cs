using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Domain.Errors;
using Todo.Domain.Options;

namespace Todo.Infrastructure.Auth;

public class RefreshTokenValidator : IRefreshTokenValidator
{
    private readonly ILogger<RefreshTokenValidator> _logger;
    private readonly RefreshTokenOptions _refreshTokenOptions;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenValidator(ILogger<RefreshTokenValidator> logger,
        IOptions<RefreshTokenOptions> refreshTokenOptions, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _refreshTokenOptions = refreshTokenOptions.Value;
        _jwtOptions = jwtOptions.Value;
    }
    
    /// <summary>
    /// Checks if the provided refresh token is legit
    /// </summary>
    public Result<JwtSecurityToken, HttpError> Validate(string refreshToken)
    {
        var validationParameters = GetTokenValidationParameters(_jwtOptions, _refreshTokenOptions.Key);
        
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
            return (validatedToken as JwtSecurityToken)!;
        }
        catch (Exception ex)
        {
            return Errors.Auth.InvalidRefreshToken(_logger, refreshToken, ex);
        }
    }
    
    public static TokenValidationParameters GetTokenValidationParameters(JwtOptions jwtOptions, string tokenSecret)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(tokenSecret)
            ),
            ClockSkew = TimeSpan.Zero
        };
    }
}