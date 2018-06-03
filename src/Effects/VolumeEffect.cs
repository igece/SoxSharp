using SoxSharp.Effects.Types;
using System.Collections.Generic;
using System.Globalization;

namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply an amplification or an attenuation to the audio signal.
  /// </summary>
  public class VolumeEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "vol"; } }

    /// <summary>
    /// The amount to change the volume, interpreted according to the given type.
    /// </summary>
    public double Gain { get; set; }

    /// <summary>
    /// How to interpret the gain value.
    /// </summary>
    public GainType? Type { get; set; }

    /// <summary>
    /// Used only on peaks to prevent clipping. Should be a value much less than 1 (e.g. 0.05 or 0.02).
    /// </summary>
    public double? Limiter { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="VolumeEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    public VolumeEffect(double gain)
    {
      Gain = gain;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="VolumeEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    /// <param name="type">How to interpret the gain value.</param>
    public VolumeEffect(double gain, GainType type)
    : this(gain)
    {
      Type = type;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="VolumeEffect"/> class.
    /// </summary>
    /// <param name="gain">Volume gain.</param>
    /// <param name="type">How to interpret the gain value.</param>
    /// <param name="limiter">Gain limiter.</param>
    public VolumeEffect(double gain, GainType type, double limiter)
    : this(gain, type)
    {
      Limiter = limiter;
    }


    /// <summary>
    /// Translate a <see cref="VolumeEffect"/> instance to a set of command arguments to be passed to SoX to be applied
    /// to the input file (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="System.String"/> containing SoX command arguments to apply a Volume effect.</returns>
    public override string ToString()
    {
      List<string> effectArgs = new List<string>(4) { Name };
      effectArgs.Add(Gain.ToString(CultureInfo.InvariantCulture));

      if (Type.HasValue)
      {
        switch (Type.Value)
        {
          case GainType.Amplitude:

            effectArgs.Add("amplitude");
            break;

          case GainType.Power:

            effectArgs.Add("power");
            break;

          case GainType.Db:

            effectArgs.Add("dB");
            break;

          default:
            // Do nothing.
            break;
        }
      }

      if (Limiter.HasValue)
        effectArgs.Add(Limiter.Value.ToString(CultureInfo.InvariantCulture));

      return string.Join(" ", effectArgs);
    }
  }
}
