namespace Domain.Entity.Auth;

public class Token
{
    public string TokenId { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public Users.User User { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ExpiredIn { get; set; }
}
