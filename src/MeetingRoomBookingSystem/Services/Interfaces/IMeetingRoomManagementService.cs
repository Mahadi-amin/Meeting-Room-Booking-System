using Domain.Entities;

namespace Services.Interfaces
{
    public interface IMeetingRoomManagementService
    {
        Task AddMeetingAsync(MeetingRoom meetingRoom);
    }
}
