namespace SoxSharp.Effects
{
  public abstract class BaseEffect : IBaseEffect
  {
    public abstract string Name { get; }

    public virtual bool IsValid() { return true; }

    public abstract override string ToString();
  }
}
