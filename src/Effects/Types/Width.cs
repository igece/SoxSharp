using System.Text;


namespace SoxSharp.Effects.Types
{
  public struct Width
  {
    public double Value;

    public WidthUnits Units;


    public Width(double width, WidthUnits units)
    {
      Value = width;
      Units = units;
    }


		public override string ToString()
		{
			StringBuilder effectArgs = new StringBuilder();
			effectArgs.Append(Value);

      switch (Units)
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

			return effectArgs.ToString();
		}
  }
}
