using Microsoft.EntityFrameworkCore;
using ReservationServiceBLL.Interfaces;
using ReservationServiceBLL.Services;
using ReservationServiceDAL;
using ReservationServiceDAL.Data;
using ReservationServiceDAL.Data.Repositories;
using ReservationServiceDAL.Interfaces;
using ReservationServiceDAL.Interfaces.Repositories;

namespace ReservationAPI
{
    public static class Startup
    {
        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReservationServiceContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("MSSQLConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<ITicketTypeRepository, TicketTypeRepository>();

            services.AddTransient<IUnitOfWorkEF, UnitOfWorkEF>();

            return services;
        }
    }
}
