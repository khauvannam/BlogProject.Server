namespace Domain.Entity.Auth;

public class Token
{
    public string UserId { get; set; }
    public Users.User User { get; set; }
    public string RefreshToken { get; set; }
    public int ExpriesIn { get; set; }
}
