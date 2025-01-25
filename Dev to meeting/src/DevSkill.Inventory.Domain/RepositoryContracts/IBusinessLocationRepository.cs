using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBusinessLocationRepository : IRepositoryBase<BusinessLocation, Guid>
    {
        (IList<BusinessLocation> data, int total, int totalDisplay) GetPagedBusinessLocations(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
