using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class WarrantyManagementService : IWarrantyManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public WarrantyManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddWarrantyAsync(Warranty warranty)
        {
            await _inventoryUnitOfWork.WarrantyRepository.AddAsync(warranty);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public IList<Warranty> GetWarranties()
        {
            return _inventoryUnitOfWork.WarrantyRepository.GetAll();
        }

        public async Task<Warranty> GetWarrantyAsync(Guid warrantyId)
        {
            return await _inventoryUnitOfWork.WarrantyRepository.GetByIdAsync(warrantyId);
        }

        public async Task UpdateWarrantyAsync(Warranty warranty)
        {
            await _inventoryUnitOfWork.WarrantyRepository.EditAsync(warranty);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteWarrantyAsync(Guid id)
        {
            await _inventoryUnitOfWork.WarrantyRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<Warranty> data, int total, int totalDisplay) GetWarranties(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.WarrantyRepository.GetPagedWarranties(pageIndex, pageSize, search, order);
        }

    }
}
