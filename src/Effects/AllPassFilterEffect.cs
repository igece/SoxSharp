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
    public override string Name { get { return "allpass"; } }

    public Frequency Frequency { get; set; }

    public Width Width { get; set; }


    public AllPassFilterEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    public AllPassFilterEffect(Frequency frequency, Width width)
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
