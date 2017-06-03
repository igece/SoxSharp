using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Boost or cut the treble (upper) frequencies of the audio using a two-pole shelving filter with a response similar
  /// to that of a standard hi-fi’s tone-controls. This is also known as shelving equalisation (EQ).
  /// </summary>
  public class TrebleEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "treble"; } }

    /// <summary>
    /// Gives the gain at whichever is the lower of ∼22 kHz and the Nyquist frequency. Its useful range is about −20
    /// (for a large cut) to +20 (for a large boost). Beware of Clipping when using a positive gain.
    /// </summary>
    public double Gain { get; set; }

    /// <summary>
    /// Sets the filter’s central frequency and so can be used to extend or reduce the frequency range to
    /// be boosted or cut. The default value is 3 kHz.
    /// </summary>
    public Frequency? Frequency { get; set; }

    /// <summary>
    /// Determines how steep is the filter’s shelf transition. In addition to the common width specification methods,
    /// ‘slope’ (the default) may be used. The useful range of ‘slope’ is about 0.3, for a gentle slope, to 1
    /// (the maximum), for a steep slope; the default value is 0.5.
    /// </summary>
    public Width? Width { get; set; }


    public TrebleEffect(double gain)
    {
      Gain = gain;
    }


    public TrebleEffect(double gain, double frequency)
    : this(gain)
    {
      Frequency = frequency;
    }


    public TrebleEffect(double gain, double frequency, double width)
    : this(gain, frequency)
    {
      Width = width;
    }


    public TrebleEffect(double gain, Frequency frequency)
    : this(gain)
    {
      Frequency = frequency;
    }


    public TrebleEffect(double gain, Frequency frequency, Width width)
    : this(gain, frequency)
    {
      Width = width;
    }


    /// <summary>
    /// Translate a <see cref="TrebleEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Treble effect.</returns>
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
