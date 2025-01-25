using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Services
{
    public interface IUnitMeasureManagementService
    {
        Task AddUnitMeasureAsync(UnitMeasure unitMeasure);
        IList<UnitMeasure> GetUnitMeasures();
        Task<UnitMeasure> GetUnitMeasureAsync(Guid unitMeasureId);
        Task UpdateUnitMeasureAsync(UnitMeasure unitMeasure);
        Task DeleteUnitMeasureAsync(Guid id);
        (IList<UnitMeasure> data, int total, int totalDisplay) GetUnitMeasures(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);

    }
}
