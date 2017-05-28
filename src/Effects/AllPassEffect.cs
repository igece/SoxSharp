using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  public class AllPassEffect : BaseEffect
  {
    public override string Name { get { return "allpass"; } }

    public Frequency Frequency { get; set; }

    public Width Width { get; set; }


    public AllPassEffect(double frequency, double width)
    {
      Frequency = frequency;
      Width = width;
    }


    public AllPassEffect(Frequency frequency, Width width)
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
