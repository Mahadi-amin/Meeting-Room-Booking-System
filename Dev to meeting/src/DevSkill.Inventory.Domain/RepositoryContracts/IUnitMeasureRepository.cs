using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IUnitMeasureRepository : IRepositoryBase<UnitMeasure, Guid>
    {
        (IList<UnitMeasure> data, int total, int totalDisplay) GetPagedUnitMeasures(int pageIndex, int pageSize,
            DataTablesSearch search, string? order);
    }
}
