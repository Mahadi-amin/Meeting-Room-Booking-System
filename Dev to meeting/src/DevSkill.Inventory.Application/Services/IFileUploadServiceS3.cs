using DevSkill.Inventory.Application.Dtos;

namespace DevSkill.Inventory.Application.Services
{
    public interface IFileUploadServiceS3
    {
        Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath);
        Task<List<string>> ListFilesAsync(string folderPath);
        Task DeleteFileAsync(string folderPath, string fileName);
        Task DeleteProductImageAsync(string fileUrl);
    }
}
