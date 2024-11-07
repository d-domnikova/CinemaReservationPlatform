using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWTAuthentication.Configuration
{
    public class JwtTokenConfiguration
    {
        private readonly IConfiguration configuration;

        public string Issuer => configuration["JwtIssuer"];

        public string Audience => configuration["JwtAudience"];

        public static DateTime ExpirationDate => DateTime.UtcNow.AddMinutes(60);

        public SymmetricSecurityKey Key =>
            new(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));

        public SigningCredentials Credentials =>
            new(Key, SecurityAlgorithms.HmacSha256);

        public JwtTokenConfiguration(IConfiguration configuration) =>
            this.configuration = configuration;
    }
}
