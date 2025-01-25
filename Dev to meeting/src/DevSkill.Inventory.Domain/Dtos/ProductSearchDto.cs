namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductSearchDto
    {
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public string? BrandId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? CurrentStockFrom { get; set; }
        public decimal? CurrentStockTo { get; set; }
    }
}
