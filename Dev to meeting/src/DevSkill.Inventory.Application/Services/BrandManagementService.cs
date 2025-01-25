using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class BrandManagementService : IBrandManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public BrandManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddBrandAsync(Brand brand)
        {
            await _inventoryUnitOfWork.BrandRepository.AddAsync(brand);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<Brand> GetBrandAsync(Guid brandId)
        {
            return await _inventoryUnitOfWork.BrandRepository.GetByIdAsync(brandId);
        }

        public IList<Brand> GetBrands()
        {
            return _inventoryUnitOfWork.BrandRepository.GetAll();
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            await _inventoryUnitOfWork.BrandRepository.EditAsync(brand);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            await _inventoryUnitOfWork.BrandRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<Brand> data, int total, int totalDisplay) GetBrands(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.BrandRepository.GetPagedBrands(pageIndex, pageSize, search, order);
        }

    }
}
