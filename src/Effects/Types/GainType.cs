namespace SoxSharp.Effects.Types
{
  public enum GainType
  {
    /// <summary>
    /// Gain is an amplitude (i.e. voltage or linear) ratio.
    /// </summary>
    Amplitude,

    /// <summary>
    /// Gain is a power (i.e. wattage or voltage-squared) ratio.
    /// </summary>
    Power,

    /// <summary>
    /// Gain is a power change in dB.
    /// </summary>
    dB
  }
}
