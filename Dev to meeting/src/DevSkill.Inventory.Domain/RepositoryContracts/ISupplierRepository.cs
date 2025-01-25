using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierRepository : IRepositoryBase<Supplier, Guid>
    {
        bool IsTitleDuplicate(string name, Guid? id = null);
        (IList<Supplier> data, int total, int totalDisplay) GetPagedSuppliers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
