﻿namespace DevSkill.Inventory.Domain.Entities
{
    public class UnitMeasure : IEntity<Guid>, ISelectable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
