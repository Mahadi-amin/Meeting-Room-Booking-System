using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public CategoryManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _inventoryUnitOfWork.CategoryRepository.AddAsync(category);
            await _inventoryUnitOfWork.SaveAsync(); 
        }

        public IList<Category> GetCategories()
        {
            return _inventoryUnitOfWork.CategoryRepository.GetAll();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _inventoryUnitOfWork.CategoryRepository.EditAsync(category);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _inventoryUnitOfWork.CategoryRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            return await _inventoryUnitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        }

        public (IList<Category> data, int total, int totalDisplay) GetCategrories(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.CategoryRepository.GetPagedCategories(pageIndex, pageSize, search, order);
        }

    }
}
