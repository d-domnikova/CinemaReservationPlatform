using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ReservationServiceDAL
{
    public class ReservationServiceContextFactory : IDesignTimeDbContextFactory<ReservationServiceContext>
    {
        public ReservationServiceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReservationServiceContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-2DRKP9DN\\SQLEXPRESS;Initial Catalog=ReservationDb;Integrated Security=true;TrustServerCertificate=True");
            
            return new ReservationServiceContext(optionsBuilder.Options);
        }
    }
}
