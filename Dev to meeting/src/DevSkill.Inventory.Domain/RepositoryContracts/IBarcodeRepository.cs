using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBarcodeRepository : IRepositoryBase<Barcode, Guid>
    {
        (IList<Barcode> data, int total, int totalDisplay) GetPagedBarcodes(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
