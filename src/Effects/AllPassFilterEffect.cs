using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Apply a two-pole all-pass filter with a central frequency and width. An all-pass filter changes the audio’s
  /// frequency to phase relationship without changing its frequency to amplitude relationship.
  /// </summary>
  public class AllPassFilterEffect : BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "allpass"; } }

    /// <summary>
    /// Central frequency.
    /// </summary>
    public Frequency Frequency { get; set; }

    /// <summary>
    /// Filter width.
    /// </summary>
    public Width Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.AllPassFilterEfect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public AllPassFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.AllPassFilterEfect"/> class.
    /// </summary>
    /// <param name="frequency">Central frequency.</param>
    /// <param name="width">Filter width.</param>
    public AllPassFilterEffect(Frequency frequency, Width width)
    {
      Frequency = frequency;
      Width = width;
    }


    /// <summary>
    /// Translate an <see cref="AllPassFilterEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply an All-Pass Filter
    /// effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);

      effectArgs.Append(" " + Frequency);
      effectArgs.Append(" " + Width);

      return effectArgs.ToString();
    }
  }
}
