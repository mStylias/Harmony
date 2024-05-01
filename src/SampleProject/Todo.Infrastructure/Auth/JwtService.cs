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
    
    private readonly JwtOptions _jwtOptions;
    private readonly RefreshTokenOptions _refreshTokenOptions;
    private readonly TimeProvider _timeProvider;

    public JwtService(IOptions<JwtOptions> jwtOptions, IOptions<RefreshTokenOptions> refreshTokenOptions, 
        TimeProvider timeProvider)
    {
        _jwtOptions = jwtOptions.Value;
        _refreshTokenOptions = refreshTokenOptions.Value;
        _timeProvider = timeProvider;
    }

    /// <summary>
    /// Generates a set of JWT tokens. Specifically, an access token and a refresh token. The refresh token expiration
    /// can optionally be provided as an argument. The reason is the following: When creating a refresh token for the first time,
    /// the expiration date is calculated based on the current date and a predefined duration for refresh tokens. After,
    /// it has been created, it's expiration date should be stored in a persistent storage. Every time the caller wants
    /// to refresh the access token, then the refresh token used for this purpose should be invalidated and get
    /// refreshed too. However, when generating a new refresh token this way it's expiration date time must be fetched
    /// from the previous refresh token . Otherwise, the refresh token would live forever, since it's
    /// expiration date time would be updated based on the current time every time a pair of access token and
    /// refresh token are generated.
    /// </summary>
    /// <param name="userId">The id of the user to store in the access token</param>
    /// <param name="refreshTokenExpiration">(Optional) The refresh token expiration datetime</param>
    /// <returns></returns>
    public AuthTokensModel GenerateTokens(string userId, DateTime? refreshTokenExpiration = null)
    {
        (string accessToken, DateTime accessTokenExpiration) = CreateAccessToken(userId);
        (string refreshToken, refreshTokenExpiration) = CreateRefreshToken(refreshTokenExpiration);
        
        return new AuthTokensModel(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration.Value);
    }
    
    private (string accessToken, DateTime expiration) CreateAccessToken(string userId)
    {
        _creationDateTime = _timeProvider.GetUtcNow().UtcDateTime;
        var expiration = _creationDateTime.Add(_jwtOptions.ExpirationTime);

        var jwtKey = _jwtOptions.Key;
        
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
        var refreshTokenKey = _refreshTokenOptions.Key;
        if (refreshTokenExpiration.HasValue == false)
        {
            refreshTokenExpiration = _creationDateTime
                .Add(_refreshTokenOptions.ExpirationTime);
        }
        
        var refreshToken = CreateJwtToken(null, CreateSigningCredentials(refreshTokenKey), 
            refreshTokenExpiration.Value);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return (tokenHandler.WriteToken(refreshToken), refreshTokenExpiration.Value);
    }

    private JwtSecurityToken CreateJwtToken(Claim[]? claims, SigningCredentials credentials, DateTime expirationDateTime)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: _creationDateTime,
            expires: expirationDateTime,
            signingCredentials: credentials
        );

        return securityToken;
    }
    
    private Claim[] CreateAccessTokenClaims(string userId)
    {
        Claim[] claims = 
        [
            new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
            new Claim(Constants.Auth.UserIdClaimType, userId)
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
