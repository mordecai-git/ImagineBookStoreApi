namespace ImagineBookStore.Core.Models.Input;

public class UserSession
{
    public int UserId { get; set; }
    public string Uid { get; set; }
    public int? BusinessId { get; set; }
}