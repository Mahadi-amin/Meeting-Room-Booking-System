using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface ICategoryManagementService
    {
        Task AddCategoryAsync(Category category);
        IList<Category> GetCategories();
        Task<Category> GetCategoryAsync(Guid categoryId);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
        (IList<Category> data, int total, int totalDisplay) GetCategrories(int pageIndex, int pageSize,
        DataTablesSearch search, string? order);

    }
}
