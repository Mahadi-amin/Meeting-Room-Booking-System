namespace DevSkill.Inventory.Domain.Entities
{
    public class Warranty : IEntity<Guid>, ISelectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WarrantyPeriod { get; set; }
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
