using System.Text;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Comparable with compression, this effect modifies an audio signal to make it sound louder. 
  /// </summary>
  public class ContrastEffect : BaseEffect
  {
    public override string Name { get { return "contrast"; } }

    /// <summary>
    /// Controls the amount of the enhancement and is a number in the range 0−100 (default 75). Note that a value of 0
    /// still gives a significant contrast enhancement.
    /// </summary>
    public double Enhancement { get; set; }


    public ContrastEffect(double enhancement)
    {
      Enhancement = enhancement;
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + Enhancement);

      return effectArgs.ToString();
    }
  }
}
