using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models.Request
{
    public class AuthenticationRequest
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
