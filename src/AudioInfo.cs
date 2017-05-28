using System;
using System.Text;


namespace SoxSharp
{
  /// <summary>
  /// Provides information about an audio file.
  /// </summary>
  public struct AudioInfo
  {
    /// <summary>
    /// Number of audio channels.
    /// </summary>
    public readonly UInt16 Channels;

    /// <summary>
    /// Audio sample rate.
    /// </summary>
    public readonly UInt32 SampleRate;

    /// <summary>
    /// Audio sample size (bits).
    /// </summary>
    public readonly UInt16 SampleSize;

    /// <summary>
    /// Audio time length. 
    /// </summary>
    public readonly TimeSpan Duration;

    /// <summary>
    /// Audio file size.
    /// </summary>
    public readonly UInt64 Size;

    /// <summary>
    /// Audio bitrate.
    /// </summary>
    public readonly UInt32 BitRate;

    /// <summary>
    /// Audio format.
    /// </summary>
    public readonly string Format;


    /// <summary>
    /// Initializes a new instance of the <see cref="SoxSharp.AudioInfo"/> class. 
    /// </summary>
    /// <param name="channels">Number of audio channels.</param>
    /// <param name="sampleRate">Audio sample rate.</param>
    /// <param name="sampleSize">Audio sample size (bits).</param>
    /// <param name="duration">Audio time length.</param>
    /// <param name="size">Audio file size</param>
    /// <param name="bitRate"></param>
    /// <param name="format">Audio format.</param>
    public AudioInfo(UInt16 channels, UInt32 sampleRate, UInt16 sampleSize, TimeSpan duration, UInt64 size, UInt32 bitRate, string format)
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
    /// Returns information about the <see cref="AudioInfo"/> instance (invalidates <see cref="Object.ToString()"/>).
    /// </summary>
    /// <returns>String containing all <see cref="AudioInfo"/> instance properties values.</returns>
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
