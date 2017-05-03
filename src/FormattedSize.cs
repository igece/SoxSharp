using System;
using System.Globalization;


namespace SoxSharp
{
  /// <summary>
  /// Utility class that converts a a SoX file size string (an integer or double value, optionally followed by a k, M or G character) to a numeric value.
  /// </summary>
  public class FormattedSize
  {
    /// <summary>
    /// Converts a SoX file size string to a <see cref="System.UInt64"/> value.
    /// </summary>
    /// <param name="formattedSize">Size string.</param>
    /// <returns>Numeric value.</returns>
    public static UInt64 ToUInt64(string formattedSize)
    {
      UInt64 multiplier = 1;

			if (formattedSize.EndsWith("k", StringComparison.InvariantCulture))
      {
        multiplier = 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      else if (formattedSize.EndsWith("M", StringComparison.InvariantCulture))
      {
        multiplier = 1024 * 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      else if (formattedSize.EndsWith("G", StringComparison.InvariantCulture))
      {
        multiplier = 1024 * 1024 * 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      return Convert.ToUInt64(double.Parse(formattedSize, CultureInfo.InvariantCulture)) * multiplier;
    }


    /// <summary>
    /// Converts a SoX file size string to a <see cref="System.UInt32"/> value.
    /// </summary>
    /// <param name="formattedSize">Size string.</param>
    /// <returns>Numeric value.</returns>
    public static UInt32 ToUInt32(string formattedSize)
    {
      UInt32 multiplier = 1;

      if (formattedSize.EndsWith("k", StringComparison.InvariantCulture))
      {
        multiplier = 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      else if (formattedSize.EndsWith("M", StringComparison.InvariantCulture))
      {
        multiplier = 1024 * 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      else if (formattedSize.EndsWith("G", StringComparison.InvariantCulture))
      {
        multiplier = 1024 * 1024 * 1024;
        formattedSize = formattedSize.Substring(0, formattedSize.Length - 1);
      }

      return Convert.ToUInt32(double.Parse(formattedSize, CultureInfo.InvariantCulture)) * multiplier;
    }
  }
}
