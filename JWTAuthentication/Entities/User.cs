using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace JWTAuthentication.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
