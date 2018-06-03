using System.Collections.Generic;
using System.Globalization;
using SoxSharp.Effects.Types;
using SoxSharp.Exceptions;


namespace SoxSharp.Effects
{
  /// <summary>
  /// Change the audio playback speed but not its pitch.<para />
  /// This effect uses the WSOLA algorithm. The audio is chopped up into segments which are then shifted in
  /// the time domain and overlapped (cross-faded) at points where their waveforms are most similar as
  /// determined by measurement of ‘least squares’.
  /// </summary>
  public class TempoEffect: BaseEffect
  {
    /// <summary>
    /// SoX effect name.
    /// </summary>
    public override string Name { get { return "tempo"; } }

    /// <summary>
    /// Ratio of new tempo to the old tempo.<para />
    /// A value of 1.1 speeds up the tempo by 10%, 0.9 slows it down by 10%.
    /// </summary>
    public double Factor { get; set; }

    /// <summary>
    /// Use tree searches instead of linear searches to find the best overlapping points. <para />
    /// This makes the effect work more quickly, but the result may not sound as good.
    /// However, if you must improve the processing speed, this generally reduces the sound
    /// quality less than reducing the search or overlap values.
    /// </summary>
    public bool? UseTreeSearches;

    /// <summary>
    /// Calculates default value of Segment based on factor, and default Search and Overlap values based on Segment.
    /// </summary>
    public OptimizationType? Optimization;

    /// <summary>
    /// Algorithm’s segment size in milliseconds.<para/>
    /// If no other flags are specified, the default value is 82 and is typically suited to making small changes to the tempo of music.
    /// For larger changes (e.g. a factor of 2), 41 ms may give a better result.
    /// </summary>
    public double? Segment { get; set; }

    /// <summary>
    /// Audio length in milliseconds over which the algorithm will search for overlapping points.<para />
    /// If no other flags are specified, the default value is 14.68. Larger values use more processing time and may or may not produce better results.<para />
    /// A practical maximum is half the value of segment. Search can be reduced to cut processing time at the risk of degrading output quality.<para />
    /// The −m, −s, and −l flags will cause the search default to be automatically adjusted based on segment.
    /// </summary>
    public double? Search { get; set; }

    /// <summary>
    /// Segment overlap length in milliseconds.<para />
    /// Default value is 12 but −m, −s, or −l flags automatically adjust overlap based on segment size.<para/>
    /// Increasing overlap increases processing time and may increase quality. A practical maximum for overlap
    /// is the value of search, with overlap typically being (at least) a little smaller than search.
    /// </summary>
    public double? Overlap { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="TempoEffect"/> class.
    /// </summary>
    /// <param name="factor">Ratio of new tempo to the old tempo.</param>
    public TempoEffect(double factor)
    {
      Factor = factor;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="TempoEffect"/> class.
    /// </summary>
    /// <param name="factor">Ratio of new tempo to the old tempo.</param>
    /// <param name="segment">Algorithm’s segment size in milliseconds.</param>
    public TempoEffect(double factor, double segment)
    : this (factor)
    {
      Segment = segment;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="TempoEffect"/> class.
    /// </summary>
    /// <param name="factor">Ratio of new tempo to the old tempo.</param>
    /// <param name="segment">Algorithm’s segment size in milliseconds.</param>
    /// <param name="search">Audio length in milliseconds over which the algorithm will search for overlapping points.</param>
    public TempoEffect(double factor, double segment, double search)
    : this(factor, segment)
    {
      Search = search;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="TempoEffect"/> class.
    /// </summary>
    /// <param name="factor">Ratio of new tempo to the old tempo.</param>
    /// <param name="segment">Algorithm’s segment size in milliseconds.</param>
    /// <param name="search">Audio length in milliseconds over which the algorithm will search for overlapping points.</param>
    /// <param name="overlap">Segment overlap length in milliseconds.</param>
    public TempoEffect(double factor, double segment, double search, double overlap)
    : this(factor, segment, search)
    {
      Overlap = overlap;
    }


    /// <summary>
    /// Translate a <see cref="TempoEffect"/> instance to a set of command arguments to be passed to SoX.
    /// (invalidates <see cref="object.ToString()"/>).
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> containing SoX command arguments to apply a Tempo effect.</returns>
    public override string ToString()
    {
      if (Search.HasValue && !Segment.HasValue)
        throw new SoxEffectException(Name, "Search value set without specifying a Segment value.");

      if (Overlap.HasValue && (!Search.HasValue || !Segment.HasValue))
        throw new SoxEffectException(Name, "Overlap value set without specifying Segment and Search values.");

      List<string> effectArgs = new List<string>(6) { Name };

      if (UseTreeSearches.HasValue)
        effectArgs.Add("-q");
      
      if (Optimization.HasValue)
      {
        switch (Optimization.Value)
        {
          case OptimizationType.Music:

            effectArgs.Add("-m");
            break;

          case OptimizationType.Speech:

            effectArgs.Add("-s");
            break;

          case OptimizationType.LinearProcessing:

            effectArgs.Add("-l");
            break;

          default:

            // Do nothing.
            break;
        }
      }

      effectArgs.Add(Factor.ToString(CultureInfo.InvariantCulture));

      if (Segment.HasValue)
        effectArgs.Add(Segment.Value.ToString(CultureInfo.InvariantCulture));

      if (Search.HasValue)
        effectArgs.Add(Search.Value.ToString(CultureInfo.InvariantCulture));

      if (Overlap.HasValue)
        effectArgs.Add(Overlap.Value.ToString(CultureInfo.InvariantCulture));

      return string.Join(" ", effectArgs);
    }
  }
}
