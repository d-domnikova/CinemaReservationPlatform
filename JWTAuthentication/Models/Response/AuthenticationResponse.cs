using JWTAuthentication.Data;
using JWTAuthentication.Entities;
using System.Text.Json.Serialization;

namespace JWTAuthentication.Models.Response
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public AuthenticationResponse(User user, List<string> roles, string jwtToken, RefreshToken refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Email = user.Email;
            Roles = roles;
            JwtToken = jwtToken;
            RefreshToken = refreshToken.Token;
            RefreshTokenExpiration = refreshToken.Expires;
        }
    }
}
