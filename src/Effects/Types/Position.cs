using System;
using System.Collections.Generic;


namespace SoxSharp.Effects.Types
{
  /// <summary>
  /// A position within the audio stream.
  /// </summary>
  public struct Position
  {
    /// <summary>
    /// Whether the position is to be interpreted relative to the start, end or the previous position if the effect accepts multiple position arguments. The audio length must be known for end-relative locations to work.
    /// </summary>
    private readonly PositionFrom? from_;

    /// <summary>
    /// Position expressed as a time value.
    /// </summary>
		private readonly TimeSpan? time_;

    /// <summary>
    /// Position expressed as number of samples.
    /// </summary>
    private readonly uint? samples_;


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.Types.Position"/> struct.
    /// </summary>
    /// <param name="time">Position expressed as a time value.</param>
    public Position(TimeSpan time)
    {
      time_ = time;
      samples_ = null;
      from_ = null;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.Types.Position"/> struct.
    /// </summary>
    /// <param name="time">Position expressed as a time value.</param>
    /// <param name="from">How</param>
    public Position(TimeSpan time, PositionFrom from)
    {
      time_ = time;
      samples_ = null;
      from_ = from;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.Types.Position"/> struct.
    /// </summary>
    /// <param name="samples">Position expressed as number of samples.</param>
    public Position(uint samples)
    {
      time_ = null;
      samples_ = samples;
      from_ = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.Types.Position"/> struct.
    /// </summary>
    /// <param name="samples">Position expressed as number of samples.</param>
    /// <param name="from">Whether the position is to be interpreted relative to the start, end or the previous position.</param>
    public Position(uint samples, PositionFrom from)
    {
      time_ = null;
      samples_ = samples;
      from_ = from;
    }


    public static implicit operator Position(uint samples)
    {
      return new Position(samples);
    }


    public static implicit operator Position(TimeSpan time)
    {
      return new Position(time);
    }


    public override string ToString()
    {
      List<string> position = new List<string>(3);

      if (from_.HasValue)
      {
        switch (from_.Value)
        {
          case PositionFrom.Start:
            position.Add("=");
            break;

          case PositionFrom.End:
            position.Add("-");
            break;

          case PositionFrom.Last:
            position.Add("+");
            break;

          default:
            // Do nothing;
            break;
        }
      }

      if (time_.HasValue)
        position.Add(time_.Value.ToString(@"hh\:mm\:ss\.ff"));
      else if (samples_.HasValue)
        position.Add(samples_.Value.ToString() + "s");

      return string.Join("", position);
    }
  }
}
