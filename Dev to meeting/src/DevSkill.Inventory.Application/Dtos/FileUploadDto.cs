namespace DevSkill.Inventory.Application.Dtos
{
    public class FileUploadDto
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
