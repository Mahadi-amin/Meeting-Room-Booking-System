using DevSkill.Inventory.Application;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;

namespace DevSkill.Inventory.Infrastructure.UnitOfWorks
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }
        public IBarcodeRepository BarcodeRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public IUnitMeasureRepository UnitMeasureRepository { get; private set; }
        public IWarrantyRepository WarrantyRepository { get; private set; }
        public IBusinessLocationRepository BusinessLocationRepository { get; private set; }
        public IPurchaseRepository PurchaseRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public IProductLocationRepository ProductLocationRepository { get; private set; }
        public ISalesRepository SalesRepository { get; private set; }
        public IStockTransferRepository StockTransferRepository { get; private set; }

        public InventoryUnitOfWork(InventoryDbContext dbContext, 
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IBarcodeRepository barcodeRepository,
            ISubCategoryRepository subCategoryRepository,
            IUnitMeasureRepository unitMeasureRepository,
            IWarrantyRepository warrantyRepository,
            IBusinessLocationRepository businessLocationRepository,
            IPurchaseRepository purchaseRepository,
            ISupplierRepository supplierRepository,
            ICustomerRepository customerRepository,
            IProductLocationRepository productLocationRepository,
            ISalesRepository salesRepository,
            IStockTransferRepository stockTransferRepository
            ) : base(dbContext)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            BrandRepository = brandRepository;
            BarcodeRepository = barcodeRepository;
            SubCategoryRepository = subCategoryRepository;
            UnitMeasureRepository = unitMeasureRepository;
            WarrantyRepository = warrantyRepository;
            BusinessLocationRepository = businessLocationRepository;
            PurchaseRepository = purchaseRepository;
            SupplierRepository = supplierRepository;
            CustomerRepository = customerRepository;
            ProductLocationRepository = productLocationRepository;
            SalesRepository = salesRepository;
            StockTransferRepository = stockTransferRepository;
        }

        public async Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductsUsingSPAsync(int pageIndex,
            int pageSize, ProductSearchDto search, string? order)
        {
            var procedureName = "GetProducts";

            var result = await SqlUtility.QueryWithStoredProcedureAsync<ProductDto>(procedureName,
                new Dictionary<string, object>
                {
                    { "PageIndex", pageIndex },
                    { "PageSize", pageSize },
                    { "OrderBy", order },
                    { "Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ? null : Guid.Parse(search.CategoryId) },
                    { "BrandId", string.IsNullOrEmpty(search.BrandId) ? null : Guid.Parse(search.BrandId) },                  
                    { "PriceFrom", search.PriceFrom != null ? search.PriceFrom : (decimal?)null },
                    { "PriceTo", search.PriceTo != null ? search.PriceTo : (decimal?)null },
                    { "CurrentStockFrom", search.CurrentStockFrom != null ? search.CurrentStockFrom : (decimal?)null },
                    { "CurrentStockTo", search.CurrentStockTo != null ? search.CurrentStockTo : (decimal?)null },
                },
                new Dictionary<string, Type>
                {
                    { "Total", typeof(int) },
                    { "TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }

    }
}


