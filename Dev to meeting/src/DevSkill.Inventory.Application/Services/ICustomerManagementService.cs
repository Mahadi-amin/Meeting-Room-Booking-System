using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface ICustomerManagementService
    {
        Task AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(Guid customerId);
        IList<Customer> GetCustomers();
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
        (IList<Customer> data, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
