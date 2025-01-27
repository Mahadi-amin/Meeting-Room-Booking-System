using Services.Dtos;
using Services.Interfaces;

namespace Services.Implementations
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath)
        {
            var filePath = Path.Combine(folderPath, fileUploadDto.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            await File.WriteAllBytesAsync(filePath, fileUploadDto.FileContent);

            return $"/meetingImages/{fileUploadDto.FileName}";
        }

    }

}
