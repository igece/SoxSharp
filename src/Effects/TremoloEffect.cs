using System.Text;
using SoxSharp.Exceptions;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a tremolo (low frequency amplitude modulation) effect to the audio.
  /// </summary>
  public class TremoloEffect : BaseEffect
  {
    public override string Name { get { return "tremolo"; } }

    /// <summary>
    /// Tremolo frequency in Hz.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Tremolo depth as a percentage (default is 40).
    /// </summary>
    public ushort? Depth { get; set; }


    public TremoloEffect(double speed)
    {
      Speed = speed;
    }


    public TremoloEffect(double speed, ushort depth)
    : this(speed)
    {
      Depth = depth;
    }


    /// <summary>
    /// Translate a <see cref="TremoloEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Tremolo effect.</returns>
    public override string ToString()
    {
      if (Depth.HasValue && (Depth.Value > 100))
        throw new SoxEffectException(Name, "Depth value shall be in the range 0-100");

      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + Speed);

      if (Depth.HasValue)
        effectArgs.Append(" " + Depth.Value);

      return effectArgs.ToString();
    }
  }
}
