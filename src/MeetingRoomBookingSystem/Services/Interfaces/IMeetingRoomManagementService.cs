using Domain;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IMeetingRoomManagementService
    {
        Task AddMeetingAsync(MeetingRoom meetingRoom);
        Task UpdateMeetingAsync(MeetingRoom meetingRoom);
        Task<MeetingRoom> GetMeetingRoomByIdAsync(Guid Id);
        (IList<MeetingRoom> data, int total, int totalDisplay) GetMeetingRooms(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}

