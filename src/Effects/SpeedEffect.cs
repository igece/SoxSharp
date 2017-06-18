using System.Globalization;
using System.Text;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Adjust the audio speed (pitch and tempo together). Technically, the speed effect only changes the sample rate
  /// information, leaving the samples themselves untouched. The rate effect is invoked automatically to resample to
  /// the output sample rate, using its default quality/speed. For higher quality or higher speed resampling, in
  /// addition to the speed effect, specify the rate effect with the desired quality option.
  /// </summary>
  public class SpeedEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "speed"; } }

    /// <summary>
    /// The ratio of the new speed to the old speed: greater than 1 speeds up, less than 1 slows down.
    /// </summary>
    public double Factor { get; set; }

    /// <summary>
    /// Interpret Factor as number of cents (i.e. 100ths of a semitone) by which the pitch (and tempo) should be
    /// adjusted: greater than 0 increases, less than 0 decreases.
    /// </summary>
    public bool? AsCents { get; set; }


    public SpeedEffect(double factor)
    {
      Factor = factor;
    }


    public SpeedEffect(double factor, bool asCents)
    : this(factor)
    {
      AsCents = asCents;
    }


    /// <summary>
    /// Translate a <see cref="SpeedEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Speed effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + Factor.ToString(CultureInfo.InvariantCulture));

      if (AsCents.HasValue && (AsCents.Value == true))
        effectArgs.Append("c");

      return effectArgs.ToString();
    }
  }
}
