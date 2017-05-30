namespace SoxSharp.Effects
{
  public interface IBaseEffect
  {
    string Name { get; }

    bool IsValid();

    string ToString();
  }
}
