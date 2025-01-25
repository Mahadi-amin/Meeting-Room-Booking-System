using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseRepository : IRepositoryBase<Purchase, Guid>
    {
        IEnumerable<Purchase> GetPurchasesByYear(int year);
        (IList<Purchase> data, int total, int totalDisplay) GetPagedPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

        (IList<Purchase> data, int total, int totalDisplay) GetCurrentMonthPagedPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
