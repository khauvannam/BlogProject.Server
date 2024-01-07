using Application.Abstraction;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Entity.Blob;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly BlobContainerClient _fileContainer;

    private readonly string _key = SecretService.GetSecret(nameof(Secret.blogblobkey));

    private readonly ILogger<FileService> _logger;
    private readonly string _storageAccount = "blogimage";

    public FileService(ILogger<FileService> logger)
    {
        _logger = logger;
        var credential = new StorageSharedKeyCredential(_storageAccount, _key);
        var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
        var blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        _fileContainer = blobServiceClient.GetBlobContainerClient("image");
    }

    public async Task<List<BlobDto>> ListAsync()
    {
        List<BlobDto> files = new();
        await foreach (var file in _fileContainer.GetBlobsAsync())
        {
            var uri = _fileContainer.Uri.ToString();
            var name = file.Name;

            var fullUri = $"{uri}/{name}";

            files.Add(
                new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                }
            );
        }

        return files;
    }

    public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
    {
        try
        {
            var client = _fileContainer.GetBlobClient(blobFilename);
            await client.DeleteAsync();
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError($"File {blobFilename} not found");
            return new BlobResponseDto
            {
                Error = true,
                Status = $"File with name {blobFilename} not found!"
            };
        }

        return new BlobResponseDto
        {
            Error = false,
            Status = $"File: {blobFilename} has been successfully deleted."
        };
    }

    public async Task<BlobResponseDto> UploadAsync(IFormFile? blob)
    {
        BlobResponseDto responseDto = new();
        try
        {
            var client = _fileContainer.GetBlobClient($"{blob.FileName}");
            await using (var data = blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            responseDto.Status = $"File {blob.FileName} has been uploaded.";
            responseDto.Error = false;
            responseDto.Blob.Uri = client.Uri.AbsoluteUri;
            responseDto.Blob.Name = client.Name;
        }
        catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
        {
            _logger.LogError(
                $"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageAccount}.'"
            );
            responseDto.Status =
                $"File with name {blob.FileName} already exists. Please use another name to store your file.";
            responseDto.Error = true;
            return responseDto;
        }

        return responseDto;
    }
}
