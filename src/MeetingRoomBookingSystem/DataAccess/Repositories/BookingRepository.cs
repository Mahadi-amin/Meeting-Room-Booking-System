using DataAccess.Data;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

}

