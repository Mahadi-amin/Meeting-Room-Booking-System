using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesRepository : Repository<Sale, Guid>, ISalesRepository
    {
        public SalesRepository(InventoryDbContext context) : base(context)
        {

        }

        public IEnumerable<Sale> GetSalesByYear(int year)
        {
            return _dbSet
                .Where(s => s.SaleDate.HasValue && s.SaleDate.Value.Year == year) 
                .ToList();
        }

        public (IList<Sale> data, int total, int totalDisplay) GetPagedSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    null,
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Customer),
                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {
                return GetDynamic(
                    x => x.Invoice.Contains(search.Value),
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Customer),
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }

        public (IList<Sale> data, int total, int totalDisplay) GetCurrentMonthPagedSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            DateTime startOfMonth = new DateTime(currentYear, currentMonth, 1);
            DateTime startOfNextMonth = startOfMonth.AddMonths(1);

            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    x => x.SaleDate >= startOfMonth && x.SaleDate < startOfNextMonth, 
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Customer),
                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {
                return GetDynamic(
                    x => x.Invoice.Contains(search.Value) && x.SaleDate >= startOfMonth && x.SaleDate < startOfNextMonth, 
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Customer),
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }


    }
}
