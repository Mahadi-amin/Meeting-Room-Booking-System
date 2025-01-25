namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal DefaultPurchasePriceInclTax { get; set; }
        public decimal DefaultSellingPriceExclTax { get; set; }
        public decimal CurrentStock { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string ProductImage { get; set; }
        public string WarrantyPeriod { get; set; }
    }
}
