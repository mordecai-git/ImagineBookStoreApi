namespace ImagineBookStore.Core.Models.Paystack;

public class TransactionResponse
{
    public string authorization_url { get; set; }
    public string access_code { get; set; }
    public string reference { get; set; }
}
