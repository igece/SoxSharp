using System;
using System.Collections.Generic;
using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Cuts portions out of the audio. Any number of positions may be given; audio is not sent to the output until the
  /// first position is reached. The effect then alternates between copying and discarding audio at each position. 
  /// Using a value of 0 for the first position parameter allows copying from the beginning of the audio.
  /// </summary>
  public class TrimEffect : BaseEffect
  {
    public override string Name { get { return "trim"; } }

    public readonly List<Position> Positions = new List<Position>();


    public TrimEffect(Position position)
    {
      Positions.Add(position);
    }


    public TrimEffect(Position position1, Position position2)
    {
      Positions.Add(position1);
      Positions.Add(position2);
    }


    public TrimEffect(Position[] positions)
    {
      Positions.AddRange(positions);
    }


    /// <summary>
    /// Translate a <see cref="TrimEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Trim effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(String.Join(" ", Positions));

      return effectArgs.ToString();
    }
  }
}
