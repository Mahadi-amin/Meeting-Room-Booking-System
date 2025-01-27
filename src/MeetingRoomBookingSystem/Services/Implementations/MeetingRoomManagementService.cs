using Domain;
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

        public async Task<MeetingRoom> GetMeetingRoomByIdAsync(Guid Id)
        {
            return await _meetingRoomUnitOfWork.MeetingRoomRepository.GetByIdAsync(Id);
        }

        public async Task UpdateMeetingAsync(MeetingRoom meetingRoom)
        {
            await _meetingRoomUnitOfWork.MeetingRoomRepository.EditAsync(meetingRoom);
            await _meetingRoomUnitOfWork.SaveAsync();
        }

        public (IList<MeetingRoom> data, int total, int totalDisplay) GetMeetingRooms(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _meetingRoomUnitOfWork.MeetingRoomRepository.GetPagedMeetingRooms(pageIndex, pageSize, search, order);
        }
    }
}
