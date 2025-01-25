namespace DevSkill.Inventory.Domain.Entities
{
    public class Supplier : IEntity<Guid>, ISelectable
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
