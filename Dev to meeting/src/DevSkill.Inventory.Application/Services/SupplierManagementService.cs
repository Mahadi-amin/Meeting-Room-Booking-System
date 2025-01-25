using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class SupplierManagementService : ISupplierManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;

        public SupplierManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            if (_inventoryUnitOfWork.SupplierRepository.IsTitleDuplicate(supplier.Name))
            {
                throw new InvalidOperationException("Product with this name already exists.");
            }
            await _inventoryUnitOfWork.SupplierRepository.AddAsync(supplier);
            await _inventoryUnitOfWork.SaveAsync();

        }

        public async Task<Supplier> GetSupplierAsync(Guid supplierId)
        {
            return await _inventoryUnitOfWork.SupplierRepository.GetByIdAsync(supplierId);
        }

        public IList<Supplier> GetSuppliers()
        {
            return _inventoryUnitOfWork.SupplierRepository.GetAll();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            await _inventoryUnitOfWork.SupplierRepository.EditAsync(supplier);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            await _inventoryUnitOfWork.SupplierRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<Supplier> data, int total, int totalDisplay) GetSuppliers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.SupplierRepository.GetPagedSuppliers(pageIndex, pageSize, search, order);
        }

        public async Task<IList<Supplier>> GetAllSuppliersAsync()
        {
            return await _inventoryUnitOfWork.SupplierRepository.GetAllAsync();
        }
    }
}
