using System.Text;


namespace SoxSharp.Effects
{
  public class NoiseReductionEffect : BaseEffect
  {
    public string File { get; set; }

    public double Amount { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
    /// </summary>
    public NoiseReductionEffect()
    : base("Noise Reduction")
    {
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder("noisered");
			effectArgs.Append(" " + File);
      effectArgs.Append(" " + Amount);

			return effectArgs.ToString();
    }
  }
}
