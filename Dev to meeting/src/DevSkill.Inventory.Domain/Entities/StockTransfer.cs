namespace DevSkill.Inventory.Domain.Entities
{
    public class StockTransfer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime TransferDate { get; set; }
        public Guid TransferFrom { get; set; }
        public string? TransferFromName { get; set; }
        public Guid TransferTo { get; set; }
        public string? TransferToName { get; set; }
        public Guid ProductId { get; set; }  
        public Product Product { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal TransferAmount { get; set; }
        public string Comments { get; set; }
        public string TransferBy { get; set; }
    }
}
