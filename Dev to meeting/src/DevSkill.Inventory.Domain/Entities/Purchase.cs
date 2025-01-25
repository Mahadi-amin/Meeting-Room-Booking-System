namespace DevSkill.Inventory.Domain.Entities
{
    public class Purchase : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateOnly? MFGDate { get; set; }
        public DateOnly? ExpDate { get; set; }
        public string? Notes { get; set; }
        public string? Invoice { get; set; }
        public decimal NetPrice { get; set; }
        public string PurchaserName { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid BusinessLocationId { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
    }
}
