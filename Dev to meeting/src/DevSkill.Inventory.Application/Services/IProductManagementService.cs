using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IProductManagementService
    {
        void AddProduct (Product product);
        IList<Product> GetProducts ();
        Task<Product> GetProduct(Guid ProductId);
        Task<Product> GetProductById(Guid ProductId);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);
        Task<decimal> GetProductPrice(Guid productId);
        Task<int> GetNearExpiryProductCountAsync();
        Task<int> GetLowStockProducts();
        List<Product> GetLowStockAndNearExpiryProductsForNotification();
        Task<int> GetTotalProductCountAsync();
        Task<(IList<ProductDto> data, int total, int totalDisplay)> GetProductsSP(int pageIndex, int pageSize,
            ProductSearchDto search, string? order);
        (IList<Product> data, int total, int totalDisplay) GetLowStockProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
        (IList<Product> data, int total, int totalDisplay) GetNearExpiryProducts(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
