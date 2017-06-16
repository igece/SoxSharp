using System.Collections.Generic;
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
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "trim"; } }

    /// <summary>
    /// The positions.
    /// </summary>
    public readonly List<Position> Positions = new List<Position>();


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.TrimEffect"/> class.
    /// </summary>
    /// <param name="position">Position to use.</param>
    public TrimEffect(Position position)
    {
      Positions.Add(position);
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.TrimEffect"/> class.
    /// </summary>
    /// <param name="position1">First position to use.</param>
    /// <param name="position2">Second position to use.</param>
    public TrimEffect(Position position1, Position position2)
    {
      Positions.Add(position1);
      Positions.Add(position2);
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.TrimEffect"/> class.
    /// </summary>
    /// <param name="positions">Positions to use.</param>
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
      return Name + " " + string.Join(" ", Positions);
    }
  }
}
