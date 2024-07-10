namespace ImagineBookStore.Model.Utilities;

/// <summary>
/// Represents information about a trace.
/// </summary>
public class TraceInfo
{
    /// <summary>
    /// Name of the file associated with the trace.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Name of the method associated with the trace.
    /// </summary>
    public string MethodName { get; set; }

    /// <summary>
    /// Line number within the file where the trace occurred.
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// Column number within the file where the trace occurred.
    /// </summary>
    public int ColumnNumber { get; set; }
}