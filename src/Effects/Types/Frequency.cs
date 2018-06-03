using System.Globalization;

namespace SoxSharp.Effects.Types
{
  public struct Frequency
  {
    private readonly double value_;

    private readonly FrequencyUnits? units_;


    public Frequency(double value)
    {
      value_ = value;
      units_ = null;
    }


    public Frequency(double value, FrequencyUnits units)
    {
      value_ = value;
      units_ = units;
    }


    public static implicit operator Frequency(double value)
    {
      return new Frequency(value);
    }


    public override string ToString()
    {
      if (units_.HasValue)
      {
        switch (units_.Value)
        {
          case FrequencyUnits.Hz:
            return value_.ToString(CultureInfo.InvariantCulture) + "h";

          case FrequencyUnits.KHz:
            return value_.ToString(CultureInfo.InvariantCulture) + "k";

          default:            
            break; // Do nothing.
        }
      }

      return value_.ToString(CultureInfo.InvariantCulture);
    }
  }
}
