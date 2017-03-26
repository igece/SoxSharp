using System.Text;


namespace SoxSharp
{
  public class OutputFormatOptions : FormatOptions
  {
    /// <summary>
    /// Compression factor for output format.
    /// </summary>
    public double? Compression { get; set; }

    /// <summary>
    /// Set comment for output file.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Add comment to output file.
    /// </summary>
    public string AddComment { get; set; }


    public OutputFormatOptions()
    : base()
    {
    }


    public override string ToString()
    {
      StringBuilder outputOptions = new StringBuilder(base.ToString());

      if (Compression.HasValue)
        outputOptions.Append(" --compression " + Compression.Value);

      if (AddComment != null)
        outputOptions.Append(" --add-comment " + AddComment);

      if (Comment != null)
        outputOptions.Append(" --comment " + Comment);

      return outputOptions.ToString();
    }
  }
}
