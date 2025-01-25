namespace DevSkill.Inventory.Domain.Entities
{
    public interface ISelectable
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
