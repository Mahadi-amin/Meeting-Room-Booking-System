using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface ISalesManagementService
    {
        Task AddSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetSalesByDateAsync(DateTime date);
        Task<string> GetCurrentMonthNetTotalFormattedAsync();
        MonthlyReportDto GetMonthlySales(int year);
        (IList<Sale> data, int total, int totalDisplay) GetSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

        (IList<Sale> data, int total, int totalDisplay) GetCurrentMonthSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
