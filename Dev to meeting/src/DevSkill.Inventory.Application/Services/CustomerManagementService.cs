using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public class CustomerManagementService : ICustomerManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public CustomerManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (_inventoryUnitOfWork.CustomerRepository.IsTitleDuplicate(customer.Name))
            {
                throw new InvalidOperationException("Customer with this name already exists.");
            }
            await _inventoryUnitOfWork.CustomerRepository.AddAsync(customer);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            return await _inventoryUnitOfWork.CustomerRepository.GetByIdAsync(customerId);
        }

        public IList<Customer> GetCustomers()
        {
            return _inventoryUnitOfWork.CustomerRepository.GetAll();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _inventoryUnitOfWork.CustomerRepository.GetByIdAsync(customer.Id);
            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            await _inventoryUnitOfWork.CustomerRepository.EditAsync(customer);
            await _inventoryUnitOfWork.SaveAsync();
        }


        public async Task DeleteCustomerAsync(Guid id)
        {
            await _inventoryUnitOfWork.CustomerRepository.RemoveAsync(id);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public (IList<Customer> data, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.CustomerRepository.GetPagedCustomers(pageIndex, pageSize, search, order);
        }

    }
}
