using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class UnitMeasureManagementService : IUnitMeasureManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public UnitMeasureManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddUnitMeasureAsync(UnitMeasure unitmeasure)
        {
            await _inventoryUnitOfWork.UnitMeasureRepository.AddAsync(unitmeasure);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<UnitMeasure> GetUnitMeasureAsync(Guid unitMeasureId)
        {
            return await _inventoryUnitOfWork.UnitMeasureRepository.GetByIdAsync(unitMeasureId);
        }

        public IList<UnitMeasure> GetUnitMeasures()
        {
            return _inventoryUnitOfWork.UnitMeasureRepository.GetAll();
        }

        public async Task UpdateUnitMeasureAsync(UnitMeasure unitMeasure)
        {
            await _inventoryUnitOfWork.UnitMeasureRepository.EditAsync(unitMeasure);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteUnitMeasureAsync(Guid id)
        {
            await _inventoryUnitOfWork.UnitMeasureRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<UnitMeasure> data, int total, int totalDisplay) GetUnitMeasures(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.UnitMeasureRepository.GetPagedUnitMeasures(pageIndex, pageSize, search, order);
        }

    }
}
