using SoxSharp.Exceptions;
using System.Collections.Generic;
using System.Globalization;

namespace SoxSharp.Effects
{
  /// <summary>
  /// Loudness control. Similar to the gain effect, but provides equalisation for the human auditory system. See
  /// http://en.wikipedia.org/wiki/Loudness for a detailed description of loudness. The gain adjustment is usually
  /// negative and the signal is equalised according to ISO 226 w.r.t. a reference level of 65dB. An alternative
  /// reference level may be given if the original audio has been equalised for some other optimal level.
  /// </summary>
  public class LoudnessEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "loudness"; } }

    /// <summary>
    /// Gain adjustment. A default gain of −10dB is used if no value is given.
    /// </summary>
    public double? Gain { get; set; }

    /// <summary>
    /// Alternative reference level to use.
    /// </summary>
    public double? Reference { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="LoudnessEffect"/> class.
    /// </summary>
    public LoudnessEffect()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoudnessEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    public LoudnessEffect(double gain)
    {
      Gain = gain;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="LoudnessEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    /// <param name="reference">How to interpret the gain value.</param>
    public LoudnessEffect(double gain, double reference)
    : this(gain)
    {
      Reference = reference;
    }


    /// <summary>
    /// Translate a <see cref="LoudnessEffect"/> instance to a set of command arguments to be passed to SoX to be applied
    /// to the input file (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="System.String"/> containing SoX command arguments to apply a Loudness effect.</returns>
    public override string ToString()
    {
      if (Reference.HasValue && !Gain.HasValue)
        throw new SoxEffectException(Name, "Reference value set without specifying a Gain value.");

      List<string> effectArgs = new List<string>(3) { Name };

      if (Gain.HasValue)
        effectArgs.Add(Gain.Value.ToString(CultureInfo.InvariantCulture));

      if (Reference.HasValue)
        effectArgs.Add(Reference.Value.ToString(CultureInfo.InvariantCulture));

      return string.Join(" ", effectArgs);
    }
  }
}

