using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Application.Auth.Common;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Infrastructure.Common.Constants;
using Todo.Infrastructure.Common.Options;

namespace Todo.Infrastructure.Auth;

public class JwtService : ITokenCreationService
{
    private DateTime _creationDateTime;
    
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IOptions<RefreshTokenOptions> _refreshTokenOptions;
    private readonly TimeProvider _timeProvider;

    public JwtService(IOptions<JwtOptions> jwtOptions, IOptions<RefreshTokenOptions> refreshTokenOptions, 
        TimeProvider timeProvider)
    {
        _jwtOptions = jwtOptions;
        _refreshTokenOptions = refreshTokenOptions;
        _timeProvider = timeProvider;
    }

    public AuthTokensModel GenerateTokens(int userId, DateTime? refreshTokenExpiration = null)
    {
        (string accessToken, DateTime accessTokenExpiration) = CreateAccessToken(userId);
        (string refreshToken, refreshTokenExpiration) = CreateRefreshToken(refreshTokenExpiration);
        
        return new AuthTokensModel(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration.Value);
    }
    
    private (string accessToken, DateTime expiration) CreateAccessToken(int userId)
    {
        _creationDateTime = _timeProvider.GetUtcNow().UtcDateTime;
        var expiration = _creationDateTime.Add(_jwtOptions.Value.ExpirationTime);

        var jwtKey = _jwtOptions.Value.Key;
        
        var token = CreateJwtToken(
            CreateAccessTokenClaims(userId),
            CreateSigningCredentials(jwtKey),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return (tokenHandler.WriteToken(token), expiration);
    }
    
    private (string refreshToken, DateTime refreshTokenExpiration) CreateRefreshToken(DateTime? refreshTokenExpiration)
    {
        var refreshTokenKey = _refreshTokenOptions.Value.Key;
        if (refreshTokenExpiration.HasValue == false)
        {
            refreshTokenExpiration = _creationDateTime
                .Add(_refreshTokenOptions.Value.ExpirationTime);
        }
        
        var refreshToken = CreateJwtToken(null, CreateSigningCredentials(refreshTokenKey), 
            refreshTokenExpiration.Value);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return (tokenHandler.WriteToken(refreshToken), refreshTokenExpiration.Value);
    }

    private JwtSecurityToken CreateJwtToken(Claim[]? claims, SigningCredentials credentials, DateTime expirationDateTime)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: claims,
            notBefore: _creationDateTime,
            expires: expirationDateTime,
            signingCredentials: credentials
        );

        return securityToken;
    }
    
    private Claim[] CreateAccessTokenClaims(int userId)
    {
        Claim[] claims = 
        [
            new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Value.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Value.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Value.Audience),
            new Claim(Constants.Auth.UserIdClaimType, userId.ToString()),
        ];

        return claims;
    }
    
    private SigningCredentials CreateSigningCredentials(string tokenSecret)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecret));
        
        var signingCredentials = new SigningCredentials(
            symmetricSecurityKey,
            SecurityAlgorithms.HmacSha256
        );

        return signingCredentials;
    }
}
