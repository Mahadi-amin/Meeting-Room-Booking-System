using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IPurchaseManagementService
    {
        Task AddPurchaseAsync(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchasesByDateAsync(DateTime date);
        Task<string> GetCurrentMonthNetTotalFormattedAsync();
        MonthlyReportDto GetMonthlyPurchases(int year);
        (IList<Purchase> data, int total, int totalDisplay) GetPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

        (IList<Purchase> data, int total, int totalDisplay) GetCurrentMonthPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
