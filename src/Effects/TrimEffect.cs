using System;
using System.Collections.Generic;
using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  public class TrimEffect : BaseEffect
  {
    public override string Name { get { return "trim"; } }

    public readonly List<Position> Positions = new List<Position>();


    public TrimEffect(Position position)
    {
      Positions.Add(position);
    }


    public TrimEffect(Position position1, Position position2)
    {
      Positions.Add(position1);
      Positions.Add(position2);
    }


    public TrimEffect(Position[] positions)
    {
      Positions.AddRange(positions);
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(String.Join(" ", Positions));

      return effectArgs.ToString();
    }
  }
}
