using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseRepository : Repository<Purchase, Guid>, IPurchaseRepository
    {
        public PurchaseRepository(InventoryDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Purchase> GetPurchasesByYear(int year)
        {
            return _dbSet
                .Where(s => s.PurchaseDate.HasValue && s.PurchaseDate.Value.Year == year) 
                .ToList();
        }
        public (IList<Purchase> data, int total, int totalDisplay) GetPagedPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    null,
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Supplier),
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
                          .Include(z => z.Supplier),
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }

        public (IList<Purchase> data, int total, int totalDisplay) GetCurrentMonthPagedPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            DateTime startOfMonth = new DateTime(currentYear, currentMonth, 1);
            DateTime startOfNextMonth = startOfMonth.AddMonths(1);

            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    x => x.PurchaseDate >= startOfMonth && x.PurchaseDate < startOfNextMonth, 
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Supplier),
                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {
                return GetDynamic(
                    x => x.Invoice.Contains(search.Value) && x.PurchaseDate >= startOfMonth && x.PurchaseDate < startOfNextMonth, 
                    order,
                    y => y.Include(z => z.Product)
                          .Include(z => z.BusinessLocation)
                          .Include(z => z.Supplier),
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }


    }
}
