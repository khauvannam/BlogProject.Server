using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BlogProject.Server.Application.Abstraction;

public interface IFileService
{
    Task<List<BlobDto>> ListAsync();
    Task<BlobResponseDto> UploadAsync(IFormFile? blob);
}