using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;

namespace DevSkill.Inventory.Application
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IBarcodeRepository BarcodeRepository { get; }
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IUnitMeasureRepository UnitMeasureRepository { get; }
        IWarrantyRepository WarrantyRepository { get; }
        IBusinessLocationRepository BusinessLocationRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IProductLocationRepository ProductLocationRepository { get; }
        ISalesRepository SalesRepository { get; }
        IStockTransferRepository StockTransferRepository { get; }
        Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductsUsingSPAsync(int pageIndex,
            int pageSize, ProductSearchDto search, string? order);

    }
}
