using DataAccess.Data;
using Domain.Entities;
using Domain.RepositoryContracts;
using Services;

namespace DataAccess.UnitOfWorks
{
    public class MeetingRoomUnitOfWork : UnitOfWork, IMeetingRoomUnitOfWork
    {
        public IMeetingRoomRepository MeetingRoomRepository { get; private set; }
        public IBookingRepository BookingRepository { get; private set; }


        public MeetingRoomUnitOfWork(ApplicationDbContext dbContext, 
            IMeetingRoomRepository meetingRoomRepository,
            IBookingRepository bookingRepository) : base(dbContext)
        {

            MeetingRoomRepository = meetingRoomRepository;
            BookingRepository = bookingRepository;
        }
    }
}
