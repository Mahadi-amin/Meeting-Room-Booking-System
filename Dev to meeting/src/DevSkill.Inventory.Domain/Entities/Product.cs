namespace DevSkill.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>, ISelectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal DefaultPurchasePriceExclTax { get; set; }
        public decimal DefaultPurchasePriceInclTax { get; set; }
        public decimal DefaultSellingPriceExclTax { get; set; }
        public string? SKU { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public decimal CurrentStock { get; set; }
        public string? ProductBrochure { get; set; }
        public string? IMEI { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsStockManaged { get; set; }
        public int? AlertQuantity { get; set; }
        public DateOnly? ExpDate { get; set; }
        public string? Rack { get; set; }
        public string? Row { get; set; }
        public string? Position { get; set; }
        public Guid? BarcodeId { get; set; }
        public Barcode? Barcode { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Guid? UnitMeasureId { get; set; }
        public UnitMeasure? UnitMeasure { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Guid? WarrantyId { get; set; }
        public Warranty Warranty { get; set; }
        public Guid? BusinessLocationId { get; set; }
        public BusinessLocation? BusinessLocation { get; set; }
    }
}


