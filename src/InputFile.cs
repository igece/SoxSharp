using System;
using System.Collections.Generic;


namespace SoxSharp
{
  /// <summary>
  /// Input format options.
  /// </summary>
  public class InputFile : FormatOptions
  {
    /// <summary>
    /// Input file name.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Input file volume adjustment factor.
    /// </summary>
    public double? Volume { get; set; }

    /// <summary>
    /// Ignore input file length given in header and read to EOF.
    /// </summary>
    public bool? IgnoreLength { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="InputFile"/> class.
    /// </summary>
    public InputFile()
    : base()
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="InputFile"/> class.
    /// </summary>
    public InputFile(string fileName)
    : base()
    {
      FileName = fileName;
    }


    /// <summary>
    /// Translate a <see cref="InputFile"/> instance to a set of command arguments to be passed to SoX to be applied to the input file (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>String containing SoX command arguments.</returns>
    public override string ToString()
    {
      List<string> inputOptions = new List<string>(4);

      string baseStr = base.ToString();

      if (!string.IsNullOrEmpty(baseStr))
        inputOptions.Add(baseStr);

      if (Volume.HasValue)
        inputOptions.Add("--volume " + Volume.Value);

      if (IgnoreLength.HasValue && (IgnoreLength.Value == true))
        inputOptions.Add("--ignore-length");

      if (!string.IsNullOrEmpty(FileName))
      {
        if (FileName.Contains(" "))
        {
          if ((Environment.OSVersion.Platform == PlatformID.Win32NT) ||
              (Environment.OSVersion.Platform == PlatformID.Win32Windows) ||
              (Environment.OSVersion.Platform == PlatformID.Win32S) ||
              (Environment.OSVersion.Platform == PlatformID.WinCE))
            inputOptions.Add("\"" + FileName + "\"");
          else
            inputOptions.Add("'" + FileName + "'");
        }
        else
          inputOptions.Add(FileName);
      }
      else
        inputOptions.Add("--null");

      return string.Join(" ", inputOptions);
    }
  }
}
