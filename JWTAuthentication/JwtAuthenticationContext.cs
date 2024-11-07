using JWTAuthentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication
{
    public class JWTAuthenticationContext : IdentityDbContext<User>
    {
        public JWTAuthenticationContext(DbContextOptions<JWTAuthenticationContext> options)
            : base(options)
        {
        }
    }
}
