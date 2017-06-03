using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a low-pass filter with 3dB point frequency. The filter can be either single-pole or double-pole
  /// (the default). The filter roll off at 6dB per pole per octave (20dB per pole per decade).
  /// </summary>
  public class LowPassFilterEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "lowpass"; } }

    public FilterType? Type { get; set; }

    public Frequency Frequency { get; set; }

    /// <summary>
    /// Applies only to double-pole filter; the default is Q = 0.707 and gives a Butterworth response.
    /// </summary>
    public Width? Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
    /// </summary>
    public LowPassFilterEffect(double frequency)
    {
      Frequency = frequency;
    }


    /// <summary>
		/// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
		/// </summary>
    public LowPassFilterEffect(double frequency, double width)
    : this(frequency)
    {
      Width = width;
    }


    public LowPassFilterEffect(Frequency frequency)
    {
      Frequency = frequency;
    }


    public LowPassFilterEffect(Frequency frequency, Width width)
    : this(frequency)
    {
      Width = width;
    }


    /// <summary>
    /// Translate a <see cref="LowPassFilterEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Low-Pass Filter effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);

      if (Type.HasValue)
      {
        switch (Type.Value)
        {
          case FilterType.SinglePole:

            effectArgs.Append(" -1");
            break;

          case FilterType.DoublePole:

            effectArgs.Append(" -2");
            break;
        }
      }

      effectArgs.Append(" " + Frequency);

      if (Width.HasValue)
        effectArgs.Append(" " + Width.Value);

      return effectArgs.ToString();
    }
  }
}