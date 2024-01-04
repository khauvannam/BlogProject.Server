using Domain.Entity.Blob;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IFileService
{
    Task<List<BlobDto>> ListAsync();
    Task<BlobResponseDto> DeleteAsync(string blobFilename);
    Task<BlobResponseDto> UploadAsync(IFormFile? blob);
}