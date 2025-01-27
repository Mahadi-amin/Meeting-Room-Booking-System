using Domain.Entities;

namespace Domain.RepositoryContracts
{
    public interface IMeetingRoomRepository : IRepositoryBase<MeetingRoom, Guid>
    {
        (IList<MeetingRoom> data, int total, int totalDisplay) GetPagedMeetingRooms(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
