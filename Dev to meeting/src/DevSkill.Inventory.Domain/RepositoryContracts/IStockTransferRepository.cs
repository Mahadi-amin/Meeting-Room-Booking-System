using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockTransferRepository : IRepositoryBase<StockTransfer, Guid>
    {
        (IList<StockTransfer> data, int total, int totalDisplay) GetPagedStockTransfers(int pageIndex, int pageSize,
                    DataTablesSearch search, string? order);

    }
}
