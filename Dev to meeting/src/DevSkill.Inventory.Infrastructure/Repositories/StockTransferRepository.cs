using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockTransferRepository : Repository<StockTransfer,Guid>, IStockTransferRepository
    {
        public StockTransferRepository(InventoryDbContext context) : base(context)
        {

        }

        public (IList<StockTransfer> data, int total, int totalDisplay) GetPagedStockTransfers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(null, order, y => y.Include(z => z.Product), pageIndex, pageSize, true);
            }
            else
            {
                if (DateTime.TryParse(search.Value, out DateTime searchDate))
                {
                    return GetDynamic(x => x.TransferDate.Date == searchDate.Date, order, null, pageIndex, pageSize, true);
                }
                else
                {
                    return (new List<StockTransfer>(), 0, 0); 
                }
            }
        }
    }
}
