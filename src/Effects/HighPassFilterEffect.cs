using System.Text;
using SoxSharp.Effects.Types;


namespace SoxSharp.Effects
{
	public class HighPassFilterEffect : BaseEffect
	{
    public FilterType? Type { get; set; }

		public Frequency Frequency { get; set; }

    public Width? Width { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Effects.NoiseReductionEffect"/> class.
    /// </summary>
    public HighPassFilterEffect(double frequency)
    : base("High-Pass Filter")
    {
      Frequency = frequency;
    }


    public HighPassFilterEffect(double frequency, FilterType type)
	  : base("High-Pass Filter")
		{
      Type = type;
      Frequency = frequency;
		} 


		public HighPassFilterEffect(double frequency, Width width)
	  : base("High-Pass Filter")
		{
      Type = FilterType.DoublePole;
      Frequency = frequency;
      Width = width;
		}


		public HighPassFilterEffect(Frequency frequency)
	  : base("High-Pass Filter")
		{
			Frequency = frequency;
		}


    public HighPassFilterEffect(Frequency frequency, FilterType type)
	  : base("High-Pass Filter")
		{
			Type = type;
			Frequency = frequency;
		}


    public HighPassFilterEffect(Frequency frequency, Width width)
		: base("High-Pass Filter")
		{
			Type = FilterType.DoublePole;
			Frequency = frequency;
			Width = width;
		}


		public override string ToString()
		{
			StringBuilder effectArgs = new StringBuilder("highpass");

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