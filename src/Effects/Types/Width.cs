using System.Text;
using System.Globalization;


namespace SoxSharp.Effects.Types
{
  public struct Width
  {
    private readonly double value_;

    private readonly WidthUnits? units_;


    public Width(double value)
    {
      value_ = value;
      units_ = null;
    }


    public Width(double value, WidthUnits units)
    {
      value_ = value;
      units_ = units;
    }


    public static implicit operator Width(double value)
    {
      return new Width(value);
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder();
      effectArgs.Append(value_.ToString(CultureInfo.InvariantCulture));

      if (units_.HasValue)
      {
        switch (units_.Value)
        {
          case WidthUnits.Hz:
            effectArgs.Append("h");
            break;

          case WidthUnits.KHz:
            effectArgs.Append("k");
            break;

          case WidthUnits.Octaves:
            effectArgs.Append("o");
            break;

          case WidthUnits.Qfactor:
            effectArgs.Append("q");
            break;
        }
      }

      return effectArgs.ToString();
    }
  }
}
