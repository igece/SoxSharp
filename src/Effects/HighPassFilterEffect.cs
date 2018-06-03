using SoxSharp.Effects.Types;
using System.Collections.Generic;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a high-pass filter with 3dB point frequency. The filter can be either single-pole or double-pole
  /// (the default). The filter roll off at 6dB per pole per octave (20dB per pole per decade).
  /// </summary>
  public class HighPassFilterEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "highpass"; } }

    /// <summary>
    /// Filter type (single-pole or double-pole).
    /// </summary>
    public FilterType? Type { get; set; }

    /// <summary>
    /// Central frequency.
    /// </summary>
    public Frequency Frequency { get; set; }

    /// <summary>
    /// Filter width. Applies only to double-pole filter; the default is Q = 0.707 and gives a Butterworth response.
    /// </summary>
    public Width? Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.HighPassFilterEffect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    public HighPassFilterEffect(double frequency)
    {
      Frequency = frequency;
    }


    /// <summary>
		/// Initializes a new instance of the <see cref="T:SoxSharp.Effects.HighPassFilterEffect"/> class.
		/// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public HighPassFilterEffect(double frequency, double width)
    : this(frequency)
    {
      Width = width;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.HighPassFilterEffect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    public HighPassFilterEffect(Frequency frequency)
    {
      Frequency = frequency;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.HighPassFilterEffect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public HighPassFilterEffect(Frequency frequency, Width width)
    : this(frequency)
    {
      Width = width;
    }


    /// <summary>
    /// Translate a <see cref="HighPassFilterEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a High-Pass Filter effect.</returns>
    public override string ToString()
    {
      List<string> effectArgs = new List<string>(4) { Name };

      if (Type.HasValue)
      {
        switch (Type.Value)
        {
          case FilterType.SinglePole:

            effectArgs.Add("-1");
            break;

          case FilterType.DoublePole:

            effectArgs.Add("-2");
            break;

          default:

            // Do nothing;
            break;
        }
      }

      effectArgs.Add(Frequency.ToString());

      if (Width.HasValue)
        effectArgs.Add(Width.Value.ToString());

      return string.Join(" ", effectArgs);
    }
  }
}
