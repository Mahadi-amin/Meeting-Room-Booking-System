namespace DevSkill.Inventory.Domain.Entities
{
    public class Sale : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime? SaleDate { get; set; }
        public string SallerName { get; set; }
        public decimal Quantity { get; set; }
        public string? Invoice { get; set; }
        public decimal NetPrice { get; set; }
        public string? Notes { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid BusinessLocationId { get; set; }
        public BusinessLocation BusinessLocation { get; set; }

    }
}
