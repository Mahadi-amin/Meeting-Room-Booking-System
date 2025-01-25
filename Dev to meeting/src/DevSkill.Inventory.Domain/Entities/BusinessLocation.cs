namespace DevSkill.Inventory.Domain.Entities
{
    public class BusinessLocation : IEntity<Guid>, ISelectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
