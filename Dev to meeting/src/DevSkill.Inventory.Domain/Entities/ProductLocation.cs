namespace DevSkill.Inventory.Domain.Entities
{
    public class ProductLocation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BusinessLocationId { get; set; }
        public decimal? CurrentQuantity { get; set; }
    }
}
