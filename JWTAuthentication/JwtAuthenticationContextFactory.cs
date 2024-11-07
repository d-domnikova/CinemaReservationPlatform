using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication
{
    public class JWTAuthenticationContextFactory : IDesignTimeDbContextFactory<JWTAuthenticationContext>
    {
        public JWTAuthenticationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JWTAuthenticationContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-2DRKP9DN\\SQLEXPRESS;Initial Catalog=JwtAuthenticationDb;Integrated Security=true;TrustServerCertificate=True");

            return new JWTAuthenticationContext(optionsBuilder.Options);
        }
    }
}
