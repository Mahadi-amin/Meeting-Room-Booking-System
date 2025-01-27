using DataAccess.Data;
using Domain;
using Domain.Entities;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public class MeetingRoomRepository : Repository<MeetingRoom, Guid>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(ApplicationDbContext context) : base(context)
        {

        }

        //public (IList<MeetingRoom> data, int total, int totalDisplay) GetPagedMeetingRooms(int pageIndex, int pageSize,
        //    DataTablesSearch search, string? order)
        //{
        //    if (string.IsNullOrWhiteSpace(search.Value))
        //        return GetDynamic(null, order, null, pageIndex, pageSize, true);
        //    else
        //        return GetDynamic(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        //}

       

        public (IList<MeetingRoom> data, int total, int totalDisplay) GetPagedMeetingRooms(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            //if (string.IsNullOrWhiteSpace(search.Value))
            //    return GetDynamic(null, order, y => y.Include(z => z.Category), pageIndex, pageSize, true);
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                // If there's no search value, return all data with the specified order
                return GetDynamic(
                    null, // No filter
                    order,
                    null,

                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {

                return GetDynamic(
                    x => x.Name.Contains(search.Value) ||
                         x.Location.Contains(search.Value) ||
                         x.Capacity.ToString().Contains(search.Value) ||
                         x.Facilities.Contains(search.Value) ||
                         x.Instructions.Contains(search.Value) ||
                         x.Color.Contains(search.Value) ||
                         x.Image.Contains(search.Value) ||
                         x.Color.ToString().Contains(search.Value) ||
                         x.TimeLimit.ToString().Contains(search.Value),



                            order,
                            null,
                            pageIndex,
                            pageSize,
                            true
                );
            }
        }

    }
}
