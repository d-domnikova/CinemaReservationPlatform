using JWTAuthentication.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace JWTAuthentication.Services.Interfaces
{
    public interface IJwtService
    {
        public JwtSecurityToken GenerateJwtToken(User user);
        public RefreshToken GenerateRefreshToken();
    }
}
