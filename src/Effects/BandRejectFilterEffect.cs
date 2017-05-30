using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a two-pole Butterworth band-reject filter with a central frequency and a (3dB-point) bandwidth.
  /// The filter roll off at 6dB per octave (20dB per decade).
  /// </summary>
  public class BandRejectFilterEffect : BaseEffect
  {
    public override string Name { get { return "bandreject"; } }

    public Frequency Frequency { get; set; }

    public Width Width { get; set; }


    public BandRejectFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    public BandRejectFilterEffect(Frequency frequency, Width width)
    {
      Frequency = frequency;
      Width = width;
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);

      effectArgs.Append(" " + Frequency);
      effectArgs.Append(" " + Width);

      return effectArgs.ToString();
    }
  }
}
