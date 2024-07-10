namespace ImagineBookStore.Model.Paystack;

/// <summary>
/// Represents the model for initiating a transaction.
/// </summary>
public class InitiateTransactionModel
{
    /// <summary>
    /// Email associated with the transaction initiation.
    /// </summary>
    public string email { get; set; }

    /// <summary>
    /// Amount for the transaction initiation.
    /// </summary>
    public string amount { get; set; }

    /// <summary>
    /// Additional metadata information for the transaction initiation.
    /// </summary>
    public string metadata { get; set; }
}