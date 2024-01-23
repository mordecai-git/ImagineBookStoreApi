namespace ImagineBookStore.Core.Models.Paystack;

public class InitiateTransactionModel
{
    public string email { get; set; }
    public string amount { get; set; }
    public string  metadata { get; set; }
}
