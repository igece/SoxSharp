using SoxSharp.Effects.Types;
using System.Collections.Generic;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a two-pole Butterworth band-pass filter with a central frequency and a (3dB-point) bandwidth.
  /// The filter roll off at 6dB per octave (20dB per decade). 
  /// </summary>
  public class BandPassFilterEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "bandpass"; } }

    /// <summary>
    /// Central frequency.
    /// </summary>
    public Frequency Frequency { get; set; }

    /// <summary>
    /// Filter width.
    /// </summary>
    public Width Width { get; set; }

    /// <summary>
    /// Selects a constant skirt gain (peak gain = Q) instead of the default constant 0dB peak gain.
    /// </summary>
    public bool? SkirtGain { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public BandPassFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public BandPassFilterEffect(Frequency frequency, Width width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// Translate an <see cref="BandPassFilterEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply an Band-Pass Filter effect.</returns>
    public override string ToString()
    {
      List<string> effectArgs = new List<string>(4) { Name };

      if (SkirtGain.HasValue && (SkirtGain.Value == true))
        effectArgs.Add("-c");

      effectArgs.Add(Frequency.ToString());
      effectArgs.Add(Width.ToString());

      return string.Join(" ", effectArgs);
    }
  }
}
