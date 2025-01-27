using Services.Dtos;

namespace Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath);
    }
}
