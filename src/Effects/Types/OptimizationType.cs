namespace SoxSharp.Effects.Types
{
  public enum OptimizationType
  {
    /// <summary>
    /// Optimize default values of segment, search and overlap for music processing.
    /// </summary>
    Music,

    /// <summary>
    /// Optimize default values of segment, search and overlap for speech processing.
    /// </summary>
    Speech,

    /// <summary>
    /// Optimize default values of segment, search and overlap for ‘linear’ processing that tends to cause more noticeable distortion but may be useful when factor is close to 1.
    /// </summary>
    LinearProcessing
  }
}
