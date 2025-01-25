using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IRepositoryBase<Category, Guid>
    {
        (IList<Category> data, int total, int totalDisplay) GetPagedCategories(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
