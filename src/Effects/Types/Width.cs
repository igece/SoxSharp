

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
      if (units_.HasValue)
      {
        switch (units_.Value)
        {
          case WidthUnits.Hz:
            return value_.ToString(CultureInfo.InvariantCulture) + "h";

          case WidthUnits.KHz:
            return value_.ToString(CultureInfo.InvariantCulture) + "k";

          case WidthUnits.Octaves:
            return value_.ToString(CultureInfo.InvariantCulture) + "o";

          case WidthUnits.Qfactor:
            return value_.ToString(CultureInfo.InvariantCulture) + "q";

          default:            
            break; // Do nothing.
        }
      }

      return value_.ToString(CultureInfo.InvariantCulture);
    }
  }
}
