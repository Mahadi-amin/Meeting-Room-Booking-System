namespace DevSkill.Inventory.Domain
{
    public interface IEmailUtility
    {
        Task SendEmailAsync(string receiverEmail, string receiverName, string subject, string body);
    }

}
