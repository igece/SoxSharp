namespace SoxSharp.Effects
{
  public abstract class BaseEffect : IBaseEffect
  {
    public abstract string Name { get; }

    public abstract override string ToString();
  }
}
