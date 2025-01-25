using DevSkill.Inventory.Application.Dtos;
using DevSkill.Inventory.Application.Services;

namespace DevSkill.Inventory.Infrastructure
{
    public class FileUploadServiceForResize : IFileUploadServiceForResize
    {
        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath)
        {
            if (fileUploadDto == null)
                throw new ArgumentNullException(nameof(fileUploadDto), "File upload details cannot be null.");

            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path cannot be null or empty.", nameof(folderPath));

            if (fileUploadDto.FileContent == null || fileUploadDto.FileContent.Length == 0)
                throw new ArgumentException("File content cannot be empty.", nameof(fileUploadDto.FileContent));

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(fileUploadDto.FileName);
            var filePath = Path.Combine(folderPath, uniqueFileName);

            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await File.WriteAllBytesAsync(filePath, fileUploadDto.FileContent);

                return Path.Combine("/TempProductImages", uniqueFileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while uploading the file.", ex);
            }
        }
    }
}
