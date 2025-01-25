using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IWarrantyRepository : IRepositoryBase<Warranty, Guid>
    {
        (IList<Warranty> data, int total, int totalDisplay) GetPagedWarranties(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
