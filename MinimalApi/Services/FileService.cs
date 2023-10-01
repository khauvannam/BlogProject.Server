using Application.Abstraction;
using Azure.Storage;
using Azure.Storage.Blobs;
using Domain.Models;

namespace MinimalApi.Services
{
    public class FileService : IFileService
    {
        private readonly string _storageAccount = "khauvannam";

        private readonly string _key =
            "d5YN024bRwdCZqwhyVN7Gxu498LhWoZkZnMqz+3rXtxPavbiXsYlZj2jy/ut1JTH0tYeKKj+7USu+AStvaZgYQ==";

        private readonly BlobContainerClient _fileContainer;

        public FileService()
        {
            var credential = new StorageSharedKeyCredential(_storageAccount, _key);
            var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
            _fileContainer = blobServiceClient.GetBlobContainerClient("image");
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            List<BlobDto> files = new List<BlobDto>();
            await foreach (var file in _fileContainer.GetBlobsAsync())
            {
                string uri = _fileContainer.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile? blob)
        {
            BlobResponseDto responseDto = new();
            BlobClient client = _fileContainer.GetBlobClient(blob.FileName);
            await using (Stream? data = blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            responseDto.Status = $"File {blob.FileName} has been uploaded.";
            responseDto.Error = false;
            responseDto.Blob.Uri = client.Uri.AbsoluteUri;
            responseDto.Blob.Name = client.Name;
            return responseDto;
        }
    }
}