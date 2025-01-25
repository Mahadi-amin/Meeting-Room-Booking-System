using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        bool IsTitleDuplicate(string title, Guid? id = null);
        Task<Product> GetProductWithCategory(Guid productId);
        Task<int> GetLowStockProductAsync();
        Task<int> GetExpiringProductsCountAsync();
        (IList<Product> data, int total, int totalDisplay) GetPagedLowStockProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

        (IList<Product> data, int total, int totalDisplay) GetPagedNearExpiryProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
