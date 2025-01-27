using Domain.RepositoryContracts;
using Domain;

namespace Services
{
    public interface IMeetingRoomUnitOfWork : IUnitOfWork
    {
        public IMeetingRoomRepository MeetingRoomRepository { get; }
        public IBookingRepository BookingRepository { get; }
    }
}
