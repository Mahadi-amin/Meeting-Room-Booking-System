using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISubCategoryRepository : IRepositoryBase<SubCategory, Guid>
    {
        (IList<SubCategory> data, int total, int totalDisplay) GetPagedSubCategories(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
