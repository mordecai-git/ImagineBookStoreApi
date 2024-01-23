namespace ImagineBookStore.Core.Models.Paystack;

public class PaystackResponse<T>
{
    public bool status { get; set; }
    public string message { get; set; }
    public T data { get; set; }
}
