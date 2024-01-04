namespace Domain.Entity.Blob;

public class BlobResponseDto
{
    public BlobDto Blob { get; set; } = new();
    public string? Status { get; set; }
    public bool Error { get; set; }
}