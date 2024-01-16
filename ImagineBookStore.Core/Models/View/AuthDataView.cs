namespace ImagineBookStore.Core.Models.View;

public class AuthDataView
{
    public UserView User { get; internal set; }
    public string Token { get; internal set; }
    public DateTime ExpiresAt { get; internal set; }
}