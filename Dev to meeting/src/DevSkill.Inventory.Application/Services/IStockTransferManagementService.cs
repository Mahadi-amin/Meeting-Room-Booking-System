using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IStockTransferManagementService
    {
        decimal? GetQuantity(Guid productId, Guid transferFrom);
        void ProcessTransfer(StockTransfer stockTransfer);
        (IList<StockTransfer> data, int total, int totalDisplay) GetStockTransfers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
