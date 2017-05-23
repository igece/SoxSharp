namespace SoxSharp.Effects
{
  public abstract class BaseEffect : IBaseEffect
  {
    public string Name { get; }


    public BaseEffect(string name)
    {
      Name = name;
    }


    public abstract override string ToString();
  }
}
