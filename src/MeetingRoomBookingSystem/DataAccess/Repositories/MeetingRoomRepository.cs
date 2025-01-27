using DataAccess.Data;
using Domain.Entities;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
