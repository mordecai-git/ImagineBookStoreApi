using System.Text.Json.Serialization;

namespace ImagineBookStore.Core.Models.View;

public class PaymentRequestView
{
    public int Id { get; set; }
    public string Authorization_Url { get; set; }
    public string Access_Code { get; set; }
    public string Reference { get; set; }

    [JsonIgnore]
    public bool IsPaid { get; set; }
}
