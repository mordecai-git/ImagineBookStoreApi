using System.Text.Json.Serialization;

namespace ImagineBookStore.Core.Models.View;

public class UserView
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Uid { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}