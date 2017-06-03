using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a two-pole Butterworth band-pass filter with a central frequency and a (3dB-point) bandwidth.
  /// The filter roll off at 6dB per octave (20dB per decade). 
  /// </summary>
  public class BandPassFilterEffect : BaseEffect
  {
    public override string Name { get { return "bandpass"; } }

    public Frequency Frequency { get; set; }

    public Width Width { get; set; }

    /// <summary>
    /// Selects a constant skirt gain (peak gain = Q) instead of the default constant 0dB peak gain.
    /// </summary>
    public bool? SkirtGain { get; set; }


    public BandPassFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


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
      StringBuilder effectArgs = new StringBuilder(Name);

      if (SkirtGain.HasValue && (SkirtGain.Value == true))
        effectArgs.Append(" -c");

      effectArgs.Append(" " + Frequency);
      effectArgs.Append(" " + Width);

      return effectArgs.ToString();
    }
  }
}
