using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesRepository : IRepositoryBase<Sale, Guid>
    {
        IEnumerable<Sale> GetSalesByYear(int year);
        (IList<Sale> data, int total, int totalDisplay) GetPagedSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

        (IList<Sale> data, int total, int totalDisplay) GetCurrentMonthPagedSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
