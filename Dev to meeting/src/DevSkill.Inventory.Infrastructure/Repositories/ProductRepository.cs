using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(InventoryDbContext context) : base(context) 
        {
            
        }
        public bool IsTitleDuplicate(string Name, Guid? id = null)
        {
            if (id.HasValue)
            {
                return GetCount(x => x.Id != id.Value && x.Name == Name) > 0;
            }
            else
            {
                return GetCount(x => x.Name == Name) > 0;
            }
        }
        public async Task<Product> GetProductWithCategory(Guid productId)
        {
            return await _dbSet
                .Include(product => product.Category)
                .Include(product => product.SubCategory)
                .Include(product => product.Brand)
                .Include(product => product.Barcode) 
                .Include(product => product.UnitMeasure)
                .Include(product => product.Warranty)
                .Include(product => product.BusinessLocation)
                .FirstOrDefaultAsync(product => product.Id == productId);
        }
        public async Task<int> GetLowStockProductAsync()
        {
            return await _dbSet
                .Where(p => p.IsStockManaged == true && p.CurrentStock < p.AlertQuantity)
                .CountAsync();
        }

        public async Task<int> GetExpiringProductsCountAsync()
        {
            // Calculate the date 60 days from now
            var dateThreshold = DateOnly.FromDateTime(DateTime.Now.AddDays(60));

            // Query the product table and count the products with ExpDate less than 60 days from now and CurrentStock > 0
            var expiringProductsCount = await _dbSet
                .Where(p => p.ExpDate.HasValue && p.ExpDate.Value <= dateThreshold && p.CurrentStock > 0)
                .CountAsync();

            return expiringProductsCount;
        }

        public (IList<Product> data, int total, int totalDisplay) GetPagedLowStockProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    x => x.IsStockManaged == true && x.CurrentStock <= x.AlertQuantity,
                    order,
                    y => y.Include(z => z.BusinessLocation), 
                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {
                return GetDynamic(
                    x => x.Name.Contains(search.Value) && x.IsStockManaged == true && x.CurrentStock <= x.AlertQuantity,
                    order,
                    y => y.Include(z => z.BusinessLocation), 
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }

        public (IList<Product> data, int total, int totalDisplay) GetPagedNearExpiryProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            var expirationThreshold = DateOnly.FromDateTime(DateTime.Now.AddDays(60));

            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(
                    x => x.ExpDate.HasValue && x.ExpDate <= expirationThreshold && x.CurrentStock > 0,
                    order,
                    y => y.Include(z => z.BusinessLocation),
                    pageIndex,
                    pageSize,
                    true
                );
            }
            else
            {
                return GetDynamic(
                    x => x.Name.Contains(search.Value) && x.ExpDate.HasValue && x.ExpDate <= expirationThreshold && x.CurrentStock > 0,
                    order,
                    y => y.Include(z => z.BusinessLocation),
                    pageIndex,
                    pageSize,
                    true
                );
            }
        }

    }
}
