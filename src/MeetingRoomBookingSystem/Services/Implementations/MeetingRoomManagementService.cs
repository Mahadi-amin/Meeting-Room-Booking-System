using Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations
{
    public class MeetingRoomManagementService : IMeetingRoomManagementService
    {
        private readonly IMeetingRoomUnitOfWork _meetingRoomUnitOfWork;


        public MeetingRoomManagementService(IMeetingRoomUnitOfWork meetingRoomUnitOfWork)
        {
            _meetingRoomUnitOfWork = meetingRoomUnitOfWork;
        }

        public async Task AddMeetingAsync(MeetingRoom meetingRoom)
        {
            await _meetingRoomUnitOfWork.MeetingRoomRepository.AddAsync(meetingRoom);
            await _meetingRoomUnitOfWork.SaveAsync();
        }
    }
}
