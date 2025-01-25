using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public ProductManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public void AddProduct(Product product)
        {
            if (_inventoryUnitOfWork.ProductRepository.IsTitleDuplicate(product.Name))
            {
                throw new InvalidOperationException("Product with this name already exists.");
            }

            _inventoryUnitOfWork.ProductRepository.Add(product);
            _inventoryUnitOfWork.Save();
        }

        public void DeleteProduct(Guid id)
        {
            _inventoryUnitOfWork.ProductRepository.Remove(id);
            _inventoryUnitOfWork.Save();
        }
       
        public async Task<Product> GetProduct(Guid ProductId)
        {
            return await _inventoryUnitOfWork.ProductRepository.GetProductWithCategory(ProductId);
        }

        public async Task<Product> GetProductById(Guid ProductId)
        {
            return await _inventoryUnitOfWork.ProductRepository.GetByIdAsync(ProductId);
        }

        public IList<Product> GetProducts()
        {
            return _inventoryUnitOfWork.ProductRepository.GetAll();
        }

        public void UpdateProduct(Product product)
        {
            _inventoryUnitOfWork.ProductRepository.Edit(product);
            _inventoryUnitOfWork.Save();
        }

        public async Task<decimal> GetProductPrice(Guid productId)
        {
            var product = await _inventoryUnitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }
            return product.DefaultPurchasePriceInclTax; 
        }

        public async Task<int> GetNearExpiryProductCountAsync()
        {
            return await _inventoryUnitOfWork.ProductRepository.GetExpiringProductsCountAsync();
        }

        public async Task<int> GetLowStockProducts()
        {
            return await _inventoryUnitOfWork.ProductRepository.GetLowStockProductAsync();
        }

        public List<Product> GetLowStockAndNearExpiryProductsForNotification()
        {
            var expirationThreshold = DateOnly.FromDateTime(DateTime.Now.AddDays(60));

            return _inventoryUnitOfWork.ProductRepository.GetAll()
                .Where(p => (p.CurrentStock <= p.AlertQuantity ||
                             (p.ExpDate.HasValue && p.ExpDate <= expirationThreshold && p.CurrentStock > 0)))
                .ToList();
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            var products = await _inventoryUnitOfWork.ProductRepository.GetAllAsync();
            return products.Count();
        }

        public (IList<Product> data, int total, int totalDisplay) GetLowStockProducts(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.ProductRepository.GetPagedLowStockProducts(pageIndex, pageSize, search, order);
        }

        public (IList<Product> data, int total, int totalDisplay) GetNearExpiryProducts(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.ProductRepository.GetPagedNearExpiryProducts(pageIndex, pageSize, search, order);
        }

        public async Task<(IList<ProductDto> data, int total, int totalDisplay)> GetProductsSP(int pageIndex,
            int pageSize, ProductSearchDto search, string? order)
        {
            return await _inventoryUnitOfWork.GetPagedProductsUsingSPAsync(pageIndex, pageSize, search, order);
        }

    }
}
