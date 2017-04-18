using System;
using System.Text;


namespace SoxSharp
{
  /// <summary>
  /// Provides information about an audio file.
  /// </summary>
  public class FileInfo
  {
    /// <summary>
    /// Number of audio channels.
    /// </summary>
    public UInt16 Channels { get; protected set; }

    /// <summary>
    /// Audio sample rate.
    /// </summary>
    public UInt32 SampleRate { get; protected set; }

    /// <summary>
    /// Audio sample size (bits).
    /// </summary>
    public UInt16 SampleSize { get; protected set; }

    /// <summary>
    /// Audio time length. 
    /// </summary>
    public TimeSpan Duration { get; protected set; }

    /// <summary>
    /// Audio file size.
    /// </summary>
    public UInt64 Size { get; protected set; }

    public UInt32 BitRate { get; protected set; }

    /// <summary>
    /// Audio format.
    /// </summary>
    public string Format { get; protected set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="SoxSharp.FileInfo"/> class. 
    /// </summary>
    /// <param name="channels">Number of audio channels.</param>
    /// <param name="sampleRate">Audio sample rate.</param>
    /// <param name="sampleSize">Audio sample size (bits).</param>
    /// <param name="duration">Audio time length.</param>
    /// <param name="size">Audio file size</param>
    /// <param name="bitRate"></param>
    /// <param name="format">Audio format.</param>
    public FileInfo(UInt16 channels, UInt32 sampleRate, UInt16 sampleSize, TimeSpan duration, UInt64 size, UInt32 bitRate, string format)
    {
      Channels = channels;
      SampleRate = sampleRate;
      SampleSize = sampleSize;
      Duration = duration;
      Size = size;
      BitRate = bitRate;
      Format = format;
    }


    /// <summary>
    /// Returns information about the <see cref="FileInfo"/> instance (invalidates <see cref="Object.ToString()"/>).
    /// </summary>
    /// <returns>String containing all <see cref="FileInfo"/> instance properties values.</returns>
    public override string ToString()
    {
      StringBuilder result = new StringBuilder();

      result.AppendLine("Channels: " + Channels);
      result.AppendLine("Sample Rate: " + SampleRate);
      result.AppendLine("Sample Size: " + SampleSize);
      result.AppendLine("Duration: " + Duration.ToString(@"hh\:mm\:ss\.ff"));
      result.AppendLine("Size: " + Size);
      result.AppendLine("BitRate: " + BitRate);
      result.AppendLine("Format: " + Format);

      return result.ToString();
    }
  }
}
