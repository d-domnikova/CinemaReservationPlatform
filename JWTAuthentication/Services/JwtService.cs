using JWTAuthentication.Configuration;
using JWTAuthentication.Entities;
using JWTAuthentication.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JWTAuthentication.Services
{
    public class JwtService : IJwtService
    {
        private JWTAuthenticationContext _dbContext;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;
        private readonly IConfiguration _configuration;

        public JwtService(JWTAuthenticationContext dbContext, JwtTokenConfiguration jwtTokenConfiguration, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _jwtTokenConfiguration = jwtTokenConfiguration;
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateJwtToken(User user) => new(
            issuer: _jwtTokenConfiguration.Issuer,
            audience: _jwtTokenConfiguration.Audience,
            claims: GetClaims(user),
            expires: JwtTokenConfiguration.ExpirationDate,
            signingCredentials: _jwtTokenConfiguration.Credentials);

        private static List<Claim> GetClaims(User user) => new()
        {
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Authentication, user.Id),
            new(ClaimTypes.Email, user.Email),
        };


        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                Expires = DateTime.UtcNow.AddDays(2),
                Created = DateTime.UtcNow
            };

            return refreshToken;

            string getUniqueToken()
            {
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                var tokenIsUnique = !_dbContext.Users.Any(u => u.RefreshTokens.Any(t => t.Token == token));

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }
    }
}
