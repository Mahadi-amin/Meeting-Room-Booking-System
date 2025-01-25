using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class BarcodeManagementService : IBarcodeManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public BarcodeManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddBarcodeAsync(Barcode barcode)
        {
            await _inventoryUnitOfWork.BarcodeRepository.AddAsync(barcode);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<Barcode> GetBarcodeAsync(Guid barcodeId)
        {
            return await _inventoryUnitOfWork.BarcodeRepository.GetByIdAsync(barcodeId);
        }

        public IList<Barcode> GetBarcodes()
        {
            return _inventoryUnitOfWork.BarcodeRepository.GetAll();
        }

        public async Task UpdateBarcodeAsync(Barcode barcode)
        {
            await _inventoryUnitOfWork.BarcodeRepository.EditAsync(barcode);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteBarcodeAsync(Guid id)
        {
            await _inventoryUnitOfWork.BarcodeRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<Barcode> data, int total, int totalDisplay) GetBarcodes(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.BarcodeRepository.GetPagedBarcodes(pageIndex, pageSize, search, order);
        }

    }
}
