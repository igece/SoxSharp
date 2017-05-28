using System.Text;


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
      StringBuilder freqStr = new StringBuilder();
      freqStr.Append(value_);

      if (units_.HasValue)
      {
        switch (units_.Value)
        {
          case FrequencyUnits.Hz:
            freqStr.Append("h");
            break;

          case FrequencyUnits.KHz:
            freqStr.Append("k");
            break;
        }
      }

      return freqStr.ToString();
    }
  }
}
