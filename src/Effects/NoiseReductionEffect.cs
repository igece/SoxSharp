using System.Text;
using System.Globalization;


namespace SoxSharp.Effects
{
  public class NoiseReductionEffect : BaseEffect
  {
    public override string Name { get { return "noisered"; } }

    public string File { get; set; }

    public double? Amount { get; set; }


    public NoiseReductionEffect(string profile)
    {
      File = profile;
    }


    public NoiseReductionEffect(string profile, double amount)
    : this(profile)
    {
      Amount = amount;
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);
      effectArgs.Append(" " + File);

      if (Amount.HasValue)
        effectArgs.Append(" " + Amount.Value.ToString(CultureInfo.InvariantCulture));

      return effectArgs.ToString();
    }
  }
}
