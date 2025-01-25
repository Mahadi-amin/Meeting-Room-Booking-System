using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class BusinessLocationManagementService : IBusinessLocationManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public BusinessLocationManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddBusinessLocationAsync(BusinessLocation businesslocation)
        {
            await _inventoryUnitOfWork.BusinessLocationRepository.AddAsync(businesslocation);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<BusinessLocation> GetBusinessLocationAsync(Guid businessLocationId)
        {
            return await _inventoryUnitOfWork.BusinessLocationRepository.GetByIdAsync(businessLocationId);
        }

        public IList<BusinessLocation> GetBusinessLocations()
        {
            return _inventoryUnitOfWork.BusinessLocationRepository.GetAll();
        }

        public async Task UpdateBusinessLocationAsync(BusinessLocation location)
        {
            await _inventoryUnitOfWork.BusinessLocationRepository.EditAsync(location);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteBusinessLocationAsync(Guid id)
        {
            await _inventoryUnitOfWork.BusinessLocationRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<BusinessLocation> data, int total, int totalDisplay) GetBusinessLocations(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.BusinessLocationRepository.GetPagedBusinessLocations(pageIndex, pageSize, search, order);
        }

    }
}
