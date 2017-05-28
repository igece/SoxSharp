using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
  public class HighPassFilterEffect : BaseEffect
  {
    public override string Name { get { return "highpass"; } }

    public FilterType? Type { get; set; }

    public Frequency Frequency { get; set; }

    public Width? Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
    /// </summary>
    public HighPassFilterEffect(double frequency)
    {
      Frequency = frequency;
    }


    /// <summary>
		/// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
		/// </summary>
    public HighPassFilterEffect(double frequency, double width)
    : this(frequency)
    {
      Width = width;
    }


    public HighPassFilterEffect(Frequency frequency)
    {
      Frequency = frequency;
    }


    public HighPassFilterEffect(Frequency frequency, Width width)
    : this(frequency)
    {
      Width = width;
    }


    public override string ToString()
    {
      StringBuilder effectArgs = new StringBuilder(Name);

      if (Type.HasValue)
      {
        switch (Type.Value)
        {
          case FilterType.SinglePole:

            effectArgs.Append(" -1");
            break;

          case FilterType.DoublePole:

            effectArgs.Append(" -2");
            break;
        }
      }

      effectArgs.Append(" " + Frequency);

      if (Width.HasValue)
        effectArgs.Append(" " + Width.Value);

      return effectArgs.ToString();
    }
  }
}