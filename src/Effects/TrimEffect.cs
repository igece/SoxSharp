using System.Text;
using System.Collections.Generic;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  public class TrimEffect : BaseEffect
  {
    public readonly List<Position> Positions = new List<Position>();


    public TrimEffect(Position position)
    : base("Trim")
    {
      Positions.Add(position);
    }


    public TrimEffect(Position[] positions)
    : base("Trim")
    {
      Positions.AddRange(positions);
    }


		public TrimEffect(Position position1, Position position2)
	  : base("Trim")
		{
			Positions.Add(position1);
      Positions.Add(position2);
		}


    public override string ToString()
    {
			StringBuilder effectArgs = new StringBuilder("trim ");
      effectArgs.Append(Position.Concatenate(Positions));

			return effectArgs.ToString();
    }
  }
}
