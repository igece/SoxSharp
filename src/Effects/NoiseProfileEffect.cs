using System.Text;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Calculate a profile of the audio for use in noise reduction and save it to file. This effect is usually run on a
  /// section of audio (obtained adding a <see cref="T:SoxSharp.Effects.TrimEffect"/> effect to the process chain) that
  /// ideally would contain silence but in fact contains noise. Such sections are typically found at the beginning or
  /// the end of a recording.
  /// </summary>
  public class NoiseProfileEffect : BaseEffect
  {
    public override string Name { get { return "noiseprof"; } }

    public string File { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseProfileEffect"/> class.
    /// </summary>
    /// <param name="file">File.</param>
    public NoiseProfileEffect(string file)
    {
      File = file;
    }


    /// <summary>
    /// Translate a <see cref="T:SoxSharp.Effects.NoiseProfileEffect"/> instance to a set of command arguments to be passed to SoX to be applied to the input file (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Noise Profile effect.</returns>
    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + File);

      return effectArgs.ToString();
    }
  }
}
