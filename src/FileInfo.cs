using System;
using System.Text;


namespace SoxSharp
{
  public class FileInfo
  {
    public UInt16 Channels { get; protected set; }

    public UInt32 SampleRate { get; protected set; }

    public UInt16 SampleSize { get; protected set; }

    public TimeSpan Duration { get; protected set; }

    public UInt64 Size { get; protected set; }

    public UInt32 BitRate { get; protected set; }

    public string Encoding { get; protected set; }


    public FileInfo(UInt16 channels, UInt32 sampleRate, UInt16 sampleSize, TimeSpan duration, UInt64 size, UInt32 bitRate, string encoding)
    {
      Channels = channels;
      SampleRate = sampleRate;
      SampleSize = sampleSize;
      Duration = duration;
      Size = size;
      BitRate = bitRate;
      Encoding = encoding;
    }


    public override string ToString()
    {
      StringBuilder result = new StringBuilder();

      result.AppendLine("Channels: " + Channels);
      result.AppendLine("Sample Rate: " + SampleRate);
      result.AppendLine("Sample Size: " + SampleSize);
      result.AppendLine("Duration: " + Duration.ToString(@"hh\:mm\:ss\.ff"));
      result.AppendLine("Size: " + Size);
      result.AppendLine("BitRate: " + BitRate);
      result.AppendLine("Encoding: " + Encoding);

      return result.ToString();
    }
  }
}
