using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Domain.Errors;
using Todo.Infrastructure.Common.Options;

namespace Todo.Infrastructure.Auth;

public class RefreshTokenValidator : IRefreshTokenValidator
{
    private readonly ILogger<RefreshTokenValidator> _logger;
    private readonly IOptions<RefreshTokenOptions> _refreshTokenOptions;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public RefreshTokenValidator(ILogger<RefreshTokenValidator> logger,
        IOptions<RefreshTokenOptions> refreshTokenOptions, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _refreshTokenOptions = refreshTokenOptions;
        _jwtOptions = jwtOptions;
    }
    
    public Result<JwtSecurityToken, HttpError> Validate(string refreshToken)
    {
        var validationParameters = GetTokenValidationParameters(_refreshTokenOptions.Value, _jwtOptions.Value);
        
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
    
    public static TokenValidationParameters GetTokenValidationParameters(RefreshTokenOptions refreshTokenOptions, 
        JwtOptions jwtOptions)
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
                Encoding.ASCII.GetBytes(refreshTokenOptions.Key)
            ),
            ClockSkew = TimeSpan.Zero
        };
    }
}