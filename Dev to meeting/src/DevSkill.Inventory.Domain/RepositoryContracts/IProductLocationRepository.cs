using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProductLocationRepository : IRepositoryBase<ProductLocation, Guid>
    {
        Task<ProductLocation?> GetByProductIdAndBusinessLocationIdAsync(Guid businessLocationId, Guid productId);
        decimal GetQuantityByProductIdAndBusinessId(Guid productId, Guid businessLocationId);
        void UpdateQuantity(Guid productId, Guid businessLocationId, decimal quantityChange);
    }
}
