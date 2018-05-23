using System.Collections.Generic;


namespace SoxSharp
{
  /// <summary>
  /// Format options to be applied to the output file. For any property not set here, SoX will infer the value from the input file.
  /// </summary>
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

    /// <summary>
    /// Normalize output file.
    /// </summary>
    public bool Normalize { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.OutputFormatOptions"/> class.
    /// </summary>
    public OutputFormatOptions()
    : base()
    {
    }


    /// <summary>
    /// Translate a <see cref="OutputFormatOptions"/> instance to a set of command arguments to be passed to SoX (adds additional command arguments to <see cref="FormatOptions.ToString()"/>).
    /// </summary>
    /// <returns>String containing SoX command arguments.</returns>
    public override string ToString()
    {
      List<string> outputOptions = new List<string>(5);

      string baseStr = base.ToString();

      if (!string.IsNullOrEmpty(baseStr))
        outputOptions.Add(baseStr);

      if (Compression.HasValue)
        outputOptions.Add("--compression " + Compression.Value);

      if (AddComment != null)
        outputOptions.Add("--add-comment \"" + AddComment + "\"");

      if (Comment != null)
        outputOptions.Add("--comment \"" + Comment + "\"");

      if (Normalize)
        outputOptions.Add("--norm");

      return string.Join(" ", outputOptions);
    }
  }
}
