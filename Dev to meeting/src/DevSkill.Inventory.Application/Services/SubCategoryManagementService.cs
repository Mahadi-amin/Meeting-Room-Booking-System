using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class SubCategoryManagementService : ISubCategoryManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public SubCategoryManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddSubCategoryAsync(SubCategory subcategory)
        {
            await _inventoryUnitOfWork.SubCategoryRepository.AddAsync(subcategory);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public IList<SubCategory> GetSubCategories()
        {
            return _inventoryUnitOfWork.SubCategoryRepository.GetAll();
        }

        public async Task<SubCategory> GetSubCategoryAsync(Guid subCategoryId)
        {
            return await _inventoryUnitOfWork.SubCategoryRepository.GetByIdAsync(subCategoryId);
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory)
        {
            await _inventoryUnitOfWork.SubCategoryRepository.EditAsync(subCategory);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteSubCategoryAsync(Guid id)
        {
            await _inventoryUnitOfWork.SubCategoryRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<SubCategory> data, int total, int totalDisplay) GetSubCategories(int pageIndex, int pageSize,
        DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.SubCategoryRepository.GetPagedSubCategories(pageIndex, pageSize, search, order);
        }

    }
}
