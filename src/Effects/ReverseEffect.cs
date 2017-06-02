namespace SoxSharp.Effects
{
  /// <summary>
  /// Reverse the audio completely. Requires temporary file space to store the audio to be reversed.
  /// </summary>
  public class ReverseEffect : BaseEffect
  {
    public override string Name { get { return "reverse"; } }


    /// <summary>
    /// Translate a <see cref="ReverseEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Reverse effect.</returns>
    public override string ToString()
    {
      return Name;
    }
  }
}
