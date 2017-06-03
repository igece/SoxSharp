namespace SoxSharp.Effects
{
  public abstract class BaseEffect : IBaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// Translate an effect class instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply the effect.</returns>
    public abstract override string ToString();
  }
}
