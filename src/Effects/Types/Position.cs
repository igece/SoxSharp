using System;
using System.Text;
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
		Position(TimeSpan time)
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
    Position(TimeSpan time, PositionFrom from)
    {
      time_ = time;
      samples_ = null;
      from_ = from;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.Types.Position"/> struct.
    /// </summary>
    /// <param name="samples">Position expressed as number of samples.</param>
    Position(uint samples)
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
    Position(uint samples, PositionFrom from)
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


    public static string Concatenate(List<Position> positions)
    {
      StringBuilder allPos = new StringBuilder();
      positions.ForEach((position) => allPos.Append(position));

      return allPos.ToString();
    }


    public override string ToString()
    {
      StringBuilder args = new StringBuilder();

      if (from_.HasValue)
      {
        switch (from_.Value)
        {
          case PositionFrom.Start:
            args.Append("=");
            break;

          case PositionFrom.End:
            args.Append("-");
            break;

          case PositionFrom.Last:
            args.Append("+");
            break;
        }
      }

      if (time_.HasValue)
        args.Append(time_.Value.ToString(@"hh\:mm\:ss\.ff"));
      else if (samples_.HasValue)
        args.Append(samples_.Value).Append("s");
      else
        throw new SoxException("Invalid Position state (neither time nor samples values)");

      return args.ToString();
    }
  }

}
