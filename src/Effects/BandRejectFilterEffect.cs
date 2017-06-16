using SoxSharp.Effects.Types;
using System.Collections.Generic;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a two-pole Butterworth band-reject filter with a central frequency and a (3dB-point) bandwidth.
  /// The filter roll off at 6dB per octave (20dB per decade).
  /// </summary>
  public class BandRejectFilterEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "bandreject"; } }

    /// <summary>
    /// Central frequency.
    /// </summary>
    public Frequency Frequency { get; set; }

    /// <summary>
    /// Filter width.
    /// </summary>
    public Width Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.BandRejectFilterEffect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public BandRejectFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.BandRejectFilterEffect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public BandRejectFilterEffect(Frequency frequency, Width width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// Translate a <see cref="BandRejectFilterEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Band-Reject Filter effect.</returns>
    public override string ToString()
    {
      List<string> effectArgs = new List<string>(3) { Name };

      effectArgs.Add(Frequency.ToString());
      effectArgs.Add(Width.ToString());

      return string.Join(" ", effectArgs);
    }
  }
}