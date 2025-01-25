using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IBusinessLocationManagementService
    {
        Task AddBusinessLocationAsync(BusinessLocation businesslocation);
        IList<BusinessLocation> GetBusinessLocations();
        Task<BusinessLocation> GetBusinessLocationAsync(Guid businessLocationId);
        Task UpdateBusinessLocationAsync(BusinessLocation location);
        Task DeleteBusinessLocationAsync(Guid id);
        (IList<BusinessLocation> data, int total, int totalDisplay) GetBusinessLocations(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
