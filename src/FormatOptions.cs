using System;
using System.Text;


namespace SoxSharp
{
  public abstract class FormatOptions
  {
    /// <summary>
    /// Force the audio file type.
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
    /// Number of channels of audio data.
    /// </summary>
    public UInt16? Channels { get; set; }
    
    /// <summary>
    /// Sample rate of audio.
    /// </summary>
    public UInt32? SampleRate { get; set; }
       
    /// <summary>
    /// Allow glob wildcard match filename.
    /// </summary>
    public bool? Glob { get; set; }


    public FormatOptions()
    {
      Glob = true;
    }


    public override string ToString()
    {
      StringBuilder formatOptions = new StringBuilder();

      if (Type.HasValue)
        formatOptions.Append(" --type " + Type.Value.ToString().ToLower());

      if (Encoding.HasValue)
      {
        switch (Encoding.Value)
        {
          case EncodingType.ALaw: formatOptions.Append(" --encoding a-law"); break;
          case EncodingType.FloatingPoint: formatOptions.Append(" --encoding floating-point"); break;
          case EncodingType.GsmFullRate: formatOptions.Append(" --encoding gsm-full-rate"); break;
          case EncodingType.ImaAdpcm: formatOptions.Append(" --encoding ima-adpcm"); break;
          case EncodingType.MsAdpcm: formatOptions.Append(" --encoding ms-pcm"); break;
          case EncodingType.MuLaw: formatOptions.Append(" --encoding mu-law"); break;
          case EncodingType.SignedInteger: formatOptions.Append(" --encoding signed-integer"); break;
          case EncodingType.UnsignedInteger: formatOptions.Append(" --encoding unsigned-integer"); break;
        }
      }

      if (ReverseNibbles.HasValue && (ReverseNibbles.Value == true))
        formatOptions.Append(" --reverse-nibbles");

      if (ReverseBits.HasValue && (ReverseBits.Value == true))
        formatOptions.Append(" --reverse-bits");

      if (ByteOrder.HasValue)
      {
        switch (ByteOrder.Value)
        {
          case ByteOrderType.BigEndian: formatOptions.Append(" --endian big"); break;
          case ByteOrderType.LittleEndian: formatOptions.Append(" --endian little"); break;
          case ByteOrderType.Swap: formatOptions.Append(" --endian swap"); break;
        }
      }

      if (Channels.HasValue)
        formatOptions.Append(" --channels " + Channels.Value);

      if (SampleRate.HasValue)
        formatOptions.Append(" --rate " + SampleRate.Value);

      if (Glob.HasValue && (Glob.Value == false))
        formatOptions.Append(" --no-glob");

      return formatOptions.ToString();
    }
  }
}
