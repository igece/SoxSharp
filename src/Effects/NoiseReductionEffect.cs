using System.Text;
using System.Globalization;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Reduce noise in the audio signal by profiling and filtering. This effect is moderately effective at removing
  /// consistent background noise such as hiss or hum. Previous to use this effect, a noise profile of the file should
  /// be obatined using <see cref="T:SoxSharp.Effects.NoiseProfileEffect"/>.
  /// </summary>
  public class NoiseReductionEffect : BaseEffect
  {
    public override string Name { get { return "noisered"; } }

    public string File { get; set; }

    /// <summary>
    /// How much noise should be removed. Valid values are between 0 and 1, with a default of 0.5. Higher numbers will
    /// remove more noise but present a greater likelihood of removing wanted components of the audio signal.
    /// </summary>
    public double? Amount { get; set; }


    public NoiseReductionEffect(string profile)
    {
      File = profile;
    }


    public NoiseReductionEffect(string profile, double amount)
    : this(profile)
    {
      Amount = amount;
    }


    /// <summary>
    /// Translate a <see cref="NoiseReductionEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Noise Reduction effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + File);

      if (Amount.HasValue)
        effectArgs.Append(" " + Amount.Value.ToString(CultureInfo.InvariantCulture));

      return effectArgs.ToString();
    }
  }
}
