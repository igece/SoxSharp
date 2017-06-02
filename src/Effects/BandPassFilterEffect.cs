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
