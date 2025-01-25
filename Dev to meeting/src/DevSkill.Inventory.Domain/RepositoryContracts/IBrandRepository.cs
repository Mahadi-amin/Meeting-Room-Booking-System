using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBrandRepository : IRepositoryBase<Brand, Guid>
    {
        (IList<Brand> data, int total, int totalDisplay) GetPagedBrands(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
