using System.Text;


namespace SoxSharp.Effects.Types
{
  public struct Frequency
  {
    public double Value;


    public FrequencyUnits Units;


		public Frequency(double frequency)
		{
			Value = frequency;
      Units = FrequencyUnits.Hz;
		}


    public Frequency(double frequency, FrequencyUnits units)
    {
      Value = frequency;
      Units = units;
    }


    public static implicit operator Frequency(double frequency)
    {
      return new Frequency(frequency);
    }


		public override string ToString()
		{
      StringBuilder freqStr = new StringBuilder();
			freqStr.Append(Value);

			switch (Units)
			{
        case FrequencyUnits.Hz:
					freqStr.Append("h");
					break;

        case FrequencyUnits.KHz:
					freqStr.Append("k");
					break;
			}

			return freqStr.ToString();
		}
  }
}
