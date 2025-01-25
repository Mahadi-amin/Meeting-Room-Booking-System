using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IBrandManagementService
    {
        Task AddBrandAsync(Brand brand);
        IList<Brand> GetBrands();
        Task<Brand> GetBrandAsync(Guid brandId);
        Task UpdateBrandAsync(Brand brand);
        Task DeleteBrandAsync(Guid id);
        (IList<Brand> data, int total, int totalDisplay) GetBrands(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
