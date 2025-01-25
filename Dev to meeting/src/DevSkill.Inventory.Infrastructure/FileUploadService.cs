using DevSkill.Inventory.Application.Dtos;
using DevSkill.Inventory.Application.Services;

namespace DevSkill.Inventory.Infrastructure
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath)
        {
            var filePath = Path.Combine(folderPath, fileUploadDto.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await File.WriteAllBytesAsync(filePath, fileUploadDto.FileContent);

            return $"/productImages/{fileUploadDto.FileName}";
        }
    }
}