namespace DevSkill.Inventory.Domain.Entities
{
    public class Barcode : IEntity<Guid>, ISelectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
