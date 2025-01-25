using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.Services
{
    public class PurchaseManagementService : IPurchaseManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        private readonly IEmailUtility _emailUtility;  

        public PurchaseManagementService(IInventoryUnitOfWork inventoryUnitOfWork, IEmailUtility emailUtility)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
            _emailUtility = emailUtility; 
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            // Add the purchase to the database
            await _inventoryUnitOfWork.PurchaseRepository.AddAsync(purchase);

            // Update product stock
            var product = await _inventoryUnitOfWork.ProductRepository.GetByIdAsync(purchase.ProductId);
            if (product != null)
            {
                product.CurrentStock += purchase.Quantity;
                _inventoryUnitOfWork.ProductRepository.Edit(product);
            }

            // Check if the ProductLocation entry exists
            var productLocation = await _inventoryUnitOfWork.ProductLocationRepository
                .GetByProductIdAndBusinessLocationIdAsync(purchase.ProductId, purchase.BusinessLocationId);

            if (productLocation != null)
            {
                // Update the existing ProductLocation quantity
                productLocation.CurrentQuantity += purchase.Quantity;
                _inventoryUnitOfWork.ProductLocationRepository.Edit(productLocation);
            }
            else
            {
                // Create a new ProductLocation entry
                var newProductLocation = new ProductLocation
                {
                    Id = Guid.NewGuid(), 
                    ProductId = purchase.ProductId,
                    BusinessLocationId = purchase.BusinessLocationId,
                    CurrentQuantity = purchase.Quantity
                };
                await _inventoryUnitOfWork.ProductLocationRepository.AddAsync(newProductLocation);
            }

            // Save changes to the database
            await _inventoryUnitOfWork.SaveAsync();

            // Retrieve the supplier associated with this purchase
            var supplier = await _inventoryUnitOfWork.SupplierRepository.GetByIdAsync(purchase.SupplierId);
            if (supplier != null)
            {
                string subject = "Purchase Order Confirmation";
                string body = $"Dear {supplier.Name},\n\nA new purchase has been made for the product '{product.Name}' with quantity {purchase.Quantity} and net price is {purchase.NetPrice}.\n\nThank you!";

                // Fire-and-forget email sending to avoid blocking
                Task.Run(() => _emailUtility.SendEmailAsync(supplier.Email, supplier.Name, subject, body));
            }
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByDateAsync(DateTime date)
        {
            var allPurchases = await _inventoryUnitOfWork.PurchaseRepository.GetAllAsync();

            return allPurchases.Where(p => p.PurchaseDate.HasValue && p.PurchaseDate.Value.Date == date.Date);
        }

        public async Task<decimal> GetCurrentMonthNetTotalAsync()
        {
            var allPurchases = await _inventoryUnitOfWork.PurchaseRepository.GetAllAsync();

            var currentDate = DateTime.Now;

            // Filter purchases for the current month and sum the NetPrice
            var currentMonthNetTotal = allPurchases
                .Where(p => p.PurchaseDate.HasValue &&
                            p.PurchaseDate.Value.Year == currentDate.Year &&
                            p.PurchaseDate.Value.Month == currentDate.Month)
                .Sum(p => p.NetPrice);

            return currentMonthNetTotal; 
        }

        public async Task<string> GetCurrentMonthNetTotalFormattedAsync()
        {
            var totalNet = await GetCurrentMonthNetTotalAsync();

            return totalNet.ToString("N0");
        }

        public MonthlyReportDto GetMonthlyPurchases(int year)
        {
            var purchasesData = _inventoryUnitOfWork.PurchaseRepository.GetPurchasesByYear(year)
                .Where(s => s.PurchaseDate.HasValue)
                .GroupBy(s => s.PurchaseDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Totalpurchase = g.Sum(s => s.NetPrice) // Assuming you have an Amount property
                })
                .ToList();

            var monthlySales = new MonthlyReportDto { Year = year };

            // Map total purchases to respective month properties
            foreach (var sales in purchasesData)
            {
                switch (sales.Month)
                {
                    case 1: monthlySales.January = sales.Totalpurchase; break;
                    case 2: monthlySales.February = sales.Totalpurchase; break;
                    case 3: monthlySales.March = sales.Totalpurchase; break;
                    case 4: monthlySales.April = sales.Totalpurchase; break;
                    case 5: monthlySales.May = sales.Totalpurchase; break;
                    case 6: monthlySales.June = sales.Totalpurchase; break;
                    case 7: monthlySales.July = sales.Totalpurchase; break;
                    case 8: monthlySales.August = sales.Totalpurchase; break;
                    case 9: monthlySales.September = sales.Totalpurchase; break;
                    case 10: monthlySales.October = sales.Totalpurchase; break;
                    case 11: monthlySales.November = sales.Totalpurchase; break;
                    case 12: monthlySales.December = sales.Totalpurchase; break;
                }
            }

            return monthlySales;
        }

        public (IList<Purchase> data, int total, int totalDisplay) GetPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.PurchaseRepository.GetPagedPurchases(pageIndex, pageSize, search, order);
        }

        public (IList<Purchase> data, int total, int totalDisplay) GetCurrentMonthPurchases(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.PurchaseRepository.GetCurrentMonthPagedPurchases(pageIndex, pageSize, search, order);
        }
    }
}
