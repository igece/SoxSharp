using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Boost or cut the bass (lower) frequencies of the audio using a two-pole shelving filter with a response
  /// similar to that of a standard hi-fi’s tone-controls. This is also known as shelving equalisation (EQ).
  /// </summary>
  public class BassEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "bass"; } }

    /// <summary>
    /// Gives the gain at 0 Hz. Its useful range is about −20 (for a large cut) to +20 (for a large boost).
    /// Beware of Clipping when using a positive gain.
    /// </summary>
    public double Gain { get; set; }

    /// <summary>
    /// Sets the filter’s central frequency and so can be used to extend or reduce the frequency range to
    /// be boosted or cut. The default value is 100 Hz.
    /// </summary>
    public Frequency? Frequency { get; set; }

    /// <summary>
    /// Determines how steep is the filter’s shelf transition. In addition to the common width specification methods,
    /// ‘slope’ (the default) may be used. The useful range of ‘slope’ is about 0.3, for a gentle slope, to 1
    /// (the maximum), for a steep slope; the default value is 0.5.
    /// </summary>
    public Width? Width { get; set; }


    public BassEffect(double gain)
    {
      Gain = gain;
    }


    public BassEffect(double gain, double frequency)
    : this(gain)
    {
      Frequency = frequency;
    }


    public BassEffect(double gain, double frequency, double width)
    : this(gain, frequency)
    {
      Width = width;
    }


    public BassEffect(double gain, Frequency frequency)
    : this(gain)
    {
      Frequency = frequency;
    }


    public BassEffect(double gain, Frequency frequency, Width width)
    : this(gain, frequency)
    {
      Width = width;
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);

      effectArgs.Append(" " + Gain);

      if (Frequency.HasValue)
        effectArgs.Append(" " + Frequency.Value);

      if (Width.HasValue)
        effectArgs.Append(" " + Width.Value);

      return effectArgs.ToString();
    }
  }
}
