using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface ISupplierManagementService
    {
        Task AddSupplierAsync(Supplier supplier);
        Task<Supplier> GetSupplierAsync(Guid supplierId);
        IList<Supplier> GetSuppliers();
        Task<IList<Supplier>> GetAllSuppliersAsync();
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid id);
        (IList<Supplier> data, int total, int totalDisplay) GetSuppliers(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
