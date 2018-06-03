using System;
using System.Collections.Generic;


namespace SoxSharp
{
  /// <summary>
  /// Format options that are applicable to both input and output files.
  /// </summary>
  public abstract class FormatOptions
  {
    /// <summary>
    /// Audio file type.
    /// </summary>
    public FileType? Type { get; set; }

    /// <summary>
    /// Audio encoding.
    /// </summary>
    public EncodingType? Encoding { get; set; }

    /// <summary>
    /// Sample size (bits).
    /// </summary>
    public UInt16? SampleSize { get; set; }

    /// <summary>
    /// Encoded nibble order.
    /// </summary>
    public bool? ReverseNibbles { get; set; }

    /// <summary>
    /// Encoded bit order.
    /// </summary>
    public bool? ReverseBits { get; set; }

    /// <summary>
    /// Encoded byte order.
    /// </summary>
    public ByteOrderType? ByteOrder { get; set; }

    /// <summary>
    /// Number of audio channels.
    /// </summary>
    public UInt16? Channels { get; set; }

    /// <summary>
    /// Audio sample rate.
    /// </summary>
    public UInt32? SampleRate { get; set; }

    /// <summary>
    /// Allow glob wildcard match filename.
    /// </summary>
    public bool? Glob { get; set; }

    /// <summary>
    /// Custom format arguments.
    /// </summary>
    public string CustomArgs { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    protected FormatOptions()
    {
    }


    /// <summary>
    /// Translate a <see cref="FormatOptions"/> instance to a set of command arguments to be passed to SoX (invalidates <see cref="Object.ToString()"/>).
    /// </summary>
    /// <returns>String containing SoX command arguments.</returns>
    public override string ToString()
    {
      List<string> formatOptions = new List<string>();

      if (Type.HasValue)
        formatOptions.Add("--type " + Type.Value.ToString().ToLower());

      if (Encoding.HasValue)
      {
        switch (Encoding.Value)
        {
          case EncodingType.ALaw: formatOptions.Add("--encoding a-law"); break;
          case EncodingType.FloatingPoint: formatOptions.Add("--encoding floating-point"); break;
          case EncodingType.GsmFullRate: formatOptions.Add("--encoding gsm-full-rate"); break;
          case EncodingType.ImaAdpcm: formatOptions.Add("--encoding ima-adpcm"); break;
          case EncodingType.MsAdpcm: formatOptions.Add("--encoding ms-pcm"); break;
          case EncodingType.MuLaw: formatOptions.Add("--encoding mu-law"); break;
          case EncodingType.SignedInteger: formatOptions.Add("--encoding signed-integer"); break;
          case EncodingType.UnsignedInteger: formatOptions.Add("--encoding unsigned-integer"); break;
          default: break; // Do nothing.
        }
      }

      if (ReverseNibbles.HasValue && (ReverseNibbles.Value == true))
        formatOptions.Add("--reverse-nibbles");

      if (ReverseBits.HasValue && (ReverseBits.Value == true))
        formatOptions.Add("--reverse-bits");

      if (ByteOrder.HasValue)
      {
        switch (ByteOrder.Value)
        {
          case ByteOrderType.BigEndian: formatOptions.Add("--endian big"); break;
          case ByteOrderType.LittleEndian: formatOptions.Add("--endian little"); break;
          case ByteOrderType.Swap: formatOptions.Add("--endian swap"); break;
          default: break; // Do nothing.
        }
      }

      if (Channels.HasValue)
        formatOptions.Add("--channels " + Channels.Value);

      if (SampleRate.HasValue)
        formatOptions.Add("--rate " + SampleRate.Value);

      if (Glob.HasValue && (Glob.Value == false))
        formatOptions.Add("--no-glob");

      if (!String.IsNullOrEmpty(CustomArgs))
        formatOptions.Add(CustomArgs);

      return string.Join(" ", formatOptions);
    }
  }
}
