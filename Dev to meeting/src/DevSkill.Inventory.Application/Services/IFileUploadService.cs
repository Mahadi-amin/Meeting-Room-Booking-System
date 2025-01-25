using DevSkill.Inventory.Application.Dtos;

namespace DevSkill.Inventory.Application.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath);
    }
}

