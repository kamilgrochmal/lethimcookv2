using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LetHimCookV2.API.Domain.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Abstractions_JsonWebToken = LetHimCookV2.API.Domain.Abstractions.JsonWebToken;
using JsonWebToken = LetHimCookV2.API.Domain.Abstractions.JsonWebToken;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace LetHimCookV2.API.Infrastructure.Auth;

public sealed class AuthManager : IAuthManager
{
    private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly IClock _clock;
    private readonly SigningCredentials _signingCredentials;
    private readonly string _issuer;

    public AuthManager(AuthOptions options, IClock clock)
    {
        var issuerSigningKey = options.IssuerSigningKey;
        if (issuerSigningKey is null)
        {
            throw new InvalidOperationException("Issuer signing key not set.");
        }

        _clock = clock;
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),  SecurityAlgorithms.HmacSha256);
        _issuer = options.Issuer;
    }

    public Abstractions_JsonWebToken CreateToken(string userId, string role = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));
        }

        var now = _clock.Current();
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };
        if (!string.IsNullOrWhiteSpace(role))
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (!string.IsNullOrWhiteSpace(audience))
        {
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
            {
                customClaims.AddRange(values.Select(value => new Claim(claim, value)));
            }

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.AddHours(1);

        var jwt = new JwtSecurityToken(
            _issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new Abstractions_JsonWebToken
        {
            AccessToken = token,
            RefreshToken = string.Empty,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            Id = userId,
            Role = role ?? string.Empty,
            Claims = claims ?? EmptyClaims
        };
    }
}

public interface IAuthManager
{
    Abstractions_JsonWebToken CreateToken(string userId, string role = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}