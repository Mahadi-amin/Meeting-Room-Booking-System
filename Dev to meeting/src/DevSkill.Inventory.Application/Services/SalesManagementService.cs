using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class SalesManagementService : ISalesManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        private readonly IEmailUtility _emailUtility;

        public SalesManagementService(IInventoryUnitOfWork inventoryUnitOfWork, 
            IEmailUtility emailUtility)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
            _emailUtility = emailUtility;
        }
        
        public async Task<IEnumerable<Sale>> GetSalesByDateAsync(DateTime date)
        {
            var allSales = await _inventoryUnitOfWork.SalesRepository.GetAllAsync();

            return allSales.Where(p => p.SaleDate.HasValue && p.SaleDate.Value.Date == date.Date);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _inventoryUnitOfWork.SalesRepository.AddAsync(sale);

            // Update product stock
            var product = await _inventoryUnitOfWork.ProductRepository.GetByIdAsync(sale.ProductId);
            if (product != null)
            {
                product.CurrentStock -= sale.Quantity;
                _inventoryUnitOfWork.ProductRepository.Edit(product);
            }

            // Check if the ProductLocation entry exists
            var productLocation = await _inventoryUnitOfWork.ProductLocationRepository
                .GetByProductIdAndBusinessLocationIdAsync(sale.ProductId, sale.BusinessLocationId);

            if (productLocation != null)
            {
                // Update the existing ProductLocation quantity
                productLocation.CurrentQuantity -= sale.Quantity;
                _inventoryUnitOfWork.ProductLocationRepository.Edit(productLocation);
            }


            // Save changes to the database
            await _inventoryUnitOfWork.SaveAsync();

            // Retrieve the customer associated with this purchase
            var customer = await _inventoryUnitOfWork.CustomerRepository.GetByIdAsync(sale.CustomerId);
            if (customer != null)
            {
                string subject = "Sales Confirmation";
                string body = $"Dear {customer.Name},\n\nA new sale has been made for the product '{product.Name}' with quantity {sale.Quantity} and net price is {sale.NetPrice}.\n\nThank you!";

                // Fire-and-forget email sending to avoid blocking
                Task.Run(() => _emailUtility.SendEmailAsync(customer.Email, customer.Name, subject, body));
            }
        }

        public async Task<decimal> GetCurrentMonthNetTotalAsync()
        {
            var allPurchases = await _inventoryUnitOfWork.SalesRepository.GetAllAsync();

            var currentDate = DateTime.Now;

            // Filter purchases for the current month and sum the NetPrice
            var currentMonthNetTotal = allPurchases
                .Where(p => p.SaleDate.HasValue &&
                            p.SaleDate.Value.Year == currentDate.Year &&
                            p.SaleDate.Value.Month == currentDate.Month)
                .Sum(p => p.NetPrice);

            return currentMonthNetTotal; // Return as decimal
        }

        public async Task<string> GetCurrentMonthNetTotalFormattedAsync()
        {
            var totalNet = await GetCurrentMonthNetTotalAsync();

            return totalNet.ToString("N0");
        }

        public MonthlyReportDto GetMonthlySales(int year)
        {
            var salesData = _inventoryUnitOfWork.SalesRepository.GetSalesByYear(year)
                .Where(s => s.SaleDate.HasValue)
                .GroupBy(s => s.SaleDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(s => s.NetPrice) // Assuming you have an Amount property
                })
                .ToList();

            var monthlySales = new MonthlyReportDto { Year = year };

            // Map total sales to respective month properties
            foreach (var sales in salesData)
            {
                switch (sales.Month)
                {
                    case 1: monthlySales.January = sales.TotalSales; break;
                    case 2: monthlySales.February = sales.TotalSales; break;
                    case 3: monthlySales.March = sales.TotalSales; break;
                    case 4: monthlySales.April = sales.TotalSales; break;
                    case 5: monthlySales.May = sales.TotalSales; break;
                    case 6: monthlySales.June = sales.TotalSales; break;
                    case 7: monthlySales.July = sales.TotalSales; break;
                    case 8: monthlySales.August = sales.TotalSales; break;
                    case 9: monthlySales.September = sales.TotalSales; break;
                    case 10: monthlySales.October = sales.TotalSales; break;
                    case 11: monthlySales.November = sales.TotalSales; break;
                    case 12: monthlySales.December = sales.TotalSales; break;
                }
            }

            return monthlySales;
        }

        public (IList<Sale> data, int total, int totalDisplay) GetSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.SalesRepository.GetPagedSales(pageIndex, pageSize, search, order);
        }

        public (IList<Sale> data, int total, int totalDisplay) GetCurrentMonthSales(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.SalesRepository.GetCurrentMonthPagedSales(pageIndex, pageSize, search, order);
        }

    }
}
