using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class StockTransferManagementService : IStockTransferManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;

        public StockTransferManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public decimal? GetQuantity(Guid productId, Guid transferFrom)
        {
            return _inventoryUnitOfWork.ProductLocationRepository.GetQuantityByProductIdAndBusinessId(productId, transferFrom);
        }

        public void ProcessTransfer(StockTransfer stockTransfer)
        {
            // Deduct quantity from TransferFrom location
            var fromLocation = _inventoryUnitOfWork.ProductLocationRepository
                .GetQuantityByProductIdAndBusinessId(stockTransfer.ProductId, stockTransfer.TransferFrom);
            if (fromLocation < stockTransfer.TransferQuantity)
            {
                throw new InvalidOperationException("Insufficient stock at TransferFrom location.");
            }

            // Update TransferFrom location
            _inventoryUnitOfWork.ProductLocationRepository.UpdateQuantity(stockTransfer.ProductId, 
                stockTransfer.TransferFrom, -stockTransfer.TransferQuantity);

            // Add quantity to TransferTo location
            var toLocation = _inventoryUnitOfWork.ProductLocationRepository.GetQuantityByProductIdAndBusinessId(
                stockTransfer.ProductId, stockTransfer.TransferTo);

            // Update TransferTo location
            _inventoryUnitOfWork.ProductLocationRepository.UpdateQuantity(stockTransfer.ProductId, 
                stockTransfer.TransferTo, stockTransfer.TransferQuantity);

            _inventoryUnitOfWork.StockTransferRepository.Add(stockTransfer);

            _inventoryUnitOfWork.Save();
        }

        public (IList<StockTransfer> data, int total, int totalDisplay) GetStockTransfers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.StockTransferRepository.GetPagedStockTransfers(pageIndex, pageSize, search, order);
        }

    }
}
