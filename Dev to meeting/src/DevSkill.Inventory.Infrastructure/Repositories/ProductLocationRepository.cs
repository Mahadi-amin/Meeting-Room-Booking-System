using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductLocationRepository : Repository<ProductLocation, Guid>, IProductLocationRepository
    {
        public ProductLocationRepository(InventoryDbContext context) : base(context)
        {

        }

        public async Task<ProductLocation> GetByProductIdAndBusinessLocationIdAsync(Guid productId, Guid businessLocationId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(pl => pl.ProductId == productId && pl.BusinessLocationId == businessLocationId);
        }

        public decimal GetQuantityByProductIdAndBusinessId(Guid productId, Guid businessLocationId)
        {
            var productLocation = _dbSet
                .FirstOrDefault(pl => pl.ProductId == productId && pl.BusinessLocationId == businessLocationId);

            return productLocation?.CurrentQuantity ?? 0;
        }

        public void UpdateQuantity(Guid productId, Guid businessLocationId, decimal quantityChange)
        {
            var productLocation = _dbSet.FirstOrDefault(pl => pl.ProductId == productId && pl.BusinessLocationId == businessLocationId);

            if (productLocation != null)
            {
                productLocation.CurrentQuantity += quantityChange; 
            }
            else
            {
                productLocation = new ProductLocation
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    BusinessLocationId = businessLocationId,
                    CurrentQuantity = quantityChange 
                };

                _dbSet.Add(productLocation); 
            }
        }

    }
}
