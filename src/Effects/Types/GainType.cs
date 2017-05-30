namespace SoxSharp.Effects.Types
{
  /// <summary>
  /// When type is amplitude or power, a gain of 1 leaves the volume unchanged, less than 1 decreases it, and greater than 1 increases it; a negative gain inverts the audio signal in addition to adjusting its volume.
  /// </summary>
  public enum GainType
  {
    /// <summary>
    /// Gain is an amplitude (i.e. voltage or linear) ratio. A gain of 1 leaves the volume unchanged, less than 1
    /// decreases it, and greater than 1 increases it; a negative gain inverts the audio signal in addition to
    /// adjusting its volume.
    /// </summary>
    Amplitude,

    /// <summary>
    /// Gain is a power (i.e. wattage or voltage-squared) ratio. A gain of 1 leaves the volume unchanged, less than 1
    /// decreases it, and greater than 1 increases it; a negative gain inverts the audio signal in addition to
    /// adjusting its volume.
    /// </summary>
    Power,

    /// <summary>
    /// Gain is a power change in dB. A gain of 0 leaves the volume unchanged, less than 0 decreases it,
    /// and greater than 0 increases it.
    /// </summary>
    Db
  }
}
