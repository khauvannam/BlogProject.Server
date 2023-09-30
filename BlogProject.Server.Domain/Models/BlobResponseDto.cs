namespace Domain.Models
{
    public class BlobResponseDto
    {

        public BlobDto Blob { get; set; }
        public string? Status { get; set; }
        public bool Error { get; set; }

    }
}
