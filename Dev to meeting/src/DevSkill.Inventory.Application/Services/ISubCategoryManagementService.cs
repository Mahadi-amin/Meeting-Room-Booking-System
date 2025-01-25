using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface ISubCategoryManagementService
    {
        Task AddSubCategoryAsync(SubCategory subCategory);
        IList<SubCategory> GetSubCategories();
        Task<SubCategory> GetSubCategoryAsync(Guid subCategoryId);
        Task UpdateSubCategoryAsync(SubCategory subCategory);
        Task DeleteSubCategoryAsync(Guid id);
        (IList<SubCategory> data, int total, int totalDisplay) GetSubCategories(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
