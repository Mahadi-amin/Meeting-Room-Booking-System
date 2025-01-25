using Amazon.S3.Model;
using Amazon.S3;
using DevSkill.Inventory.Application.Dtos;
using DevSkill.Inventory.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Infrastructure
{
    public class FileUploadServiceS3 : IFileUploadServiceS3
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger<FileUploadService> _logger;

        public FileUploadServiceS3(IConfiguration configuration, ILogger<FileUploadService> logger)
        {
            _bucketName = configuration["AWS:BucketName"];
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            var region = configuration["AWS:Region"];

            var awsOptions = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
            };

            _s3Client = new AmazonS3Client(accessKey, secretKey, awsOptions);
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto, string folderPath)
        {
            try
            {
                folderPath = folderPath.Replace("\\", "/").Trim('/');

                var fileKey = $"{folderPath}/{Uri.EscapeDataString(fileUploadDto.FileName)}";

                using (var stream = new MemoryStream(fileUploadDto.FileContent))
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = fileKey,
                        InputStream = stream,
                        ContentType = fileUploadDto.ContentType,
                        AutoCloseStream = true
                    };

                    await _s3Client.PutObjectAsync(putRequest);
                    _logger.LogInformation($"File uploaded to S3: {fileKey}");

                    return $"https://{_bucketName}.s3.{_s3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{fileKey}";
                }
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError($"Error encountered on S3 operation: {ex.Message}");
                throw new Exception("Error uploading file to S3.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unknown error encountered: {ex.Message}");
                throw new Exception("Unknown error occurred while uploading the file.", ex);
            }
        }

        public async Task<List<string>> ListFilesAsync(string folderPath)
        {
            var fileKeys = new List<string>();
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = folderPath.Trim('/') + "/" 
                };

                var response = await _s3Client.ListObjectsV2Async(request);
                foreach (var entry in response.S3Objects)
                {
                    fileKeys.Add(entry.Key);
                }
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError($"Error encountered on S3 operation: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unknown error encountered: {ex.Message}");
                throw;
            }
            return fileKeys;
        }

        public async Task DeleteFileAsync(string folderPath, string fileName)
        {
            try
            {
                var fileKey = $"{folderPath.Trim('/')}/{Uri.EscapeDataString(fileName)}";

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
                _logger.LogInformation($"File deleted from S3: {fileKey}");
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError($"Error encountered on S3 operation: {ex.Message}");
                throw new Exception("Error deleting file from S3.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unknown error encountered: {ex.Message}");
                throw new Exception("Unknown error occurred while deleting the file.", ex);
            }
        }

        public async Task DeleteProductImageAsync(string fileUrl)
        {
            try
            {
                var uri = new Uri(fileUrl);
                var fileKey = uri.AbsolutePath.TrimStart('/');

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey 
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
                _logger.LogInformation($"File deleted from S3: {fileKey}");
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError($"Error encountered on S3 operation: {ex.Message}");
                throw new Exception("Error deleting file from S3.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unknown error encountered: {ex.Message}");
                throw new Exception("Unknown error occurred while deleting the file.", ex);
            }
        }
    }
}
