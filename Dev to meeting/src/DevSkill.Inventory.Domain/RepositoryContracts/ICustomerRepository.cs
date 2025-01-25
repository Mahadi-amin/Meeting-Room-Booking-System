using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer, Guid>
    {
        bool IsTitleDuplicate(string name, Guid? id = null);
        (IList<Customer> data, int total, int totalDisplay) GetPagedCustomers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
