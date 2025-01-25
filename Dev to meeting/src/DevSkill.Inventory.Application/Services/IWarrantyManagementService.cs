using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IWarrantyManagementService
    {
        Task AddWarrantyAsync(Warranty warranty);
        IList<Warranty> GetWarranties();
        Task<Warranty> GetWarrantyAsync(Guid warrantyId);
        Task UpdateWarrantyAsync(Warranty warranty);
        Task DeleteWarrantyAsync(Guid id);
        (IList<Warranty> data, int total, int totalDisplay) GetWarranties(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
