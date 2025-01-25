using DevSkill.Inventory.Application.Dtos;

namespace DevSkill.Inventory.Application.Services
{
    public interface IFileUploadServiceForResize
    {
        Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath);
    }
}
