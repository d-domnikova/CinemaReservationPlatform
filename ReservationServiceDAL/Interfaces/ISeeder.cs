using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReservationServiceDAL.Interfaces
{
    public interface ISeeder<T> where T : class
    {
        void Seed(EntityTypeBuilder<T> builder);
    }
}
