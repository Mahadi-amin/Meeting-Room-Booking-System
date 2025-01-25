using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class BusinessLocationRepository : Repository<BusinessLocation, Guid>, IBusinessLocationRepository
    {
        public BusinessLocationRepository(InventoryDbContext context) : base(context)
        {

        }

        public (IList<BusinessLocation> data, int total, int totalDisplay) GetPagedBusinessLocations(int pageIndex, int pageSize,
            DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
                return GetDynamic(null, order, null, pageIndex, pageSize, true);
            else
                return GetDynamic(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }

    }
}
