using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IFileService
{
    // Task<List<BlobDto>> ListAsync();
    Task<BlobResponseDto> UploadAsync(IFormFile? blob);
}