using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IBarcodeManagementService
    {
        Task AddBarcodeAsync(Barcode barcode);
        IList<Barcode> GetBarcodes();
        Task<Barcode> GetBarcodeAsync(Guid barcodeId);
        Task UpdateBarcodeAsync(Barcode barcode);
        Task DeleteBarcodeAsync(Guid id);
        (IList<Barcode> data, int total, int totalDisplay) GetBarcodes(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
