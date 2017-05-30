namespace SoxSharp
{
  /// <summary>
  /// Audio file formats.
  /// </summary>
  public enum FileType
  {
    /// <summary>
    /// Amiga 8SVX musical instrument description format.
    /// </summary>
    SVX,
    /// <summary>
    /// Audio Interchage File Format. Used on old Apple Macs, Apple IIc/IIgs and SGI. SoX’s AIFF support does not
    /// include multiple audio chunks, or the 8SVX musical instrument description format. AIFF files are multimedia
    /// archives and can have multiple audio and picture chunks — you may need a separate archiver to work with them.
    /// With Mac OS X, AIFF has been superseded by CAF.
    /// </summary>
    AIF,
    /// <summary>
    /// AIFF-C (not compressed), defined in DAVIC 1.4 Part 9 Annex B. Format based on AIFF that was created to allow
    /// handling compressed audio. It can also handle little endian uncompressed linear data that is often referred to
    /// as sowt encoding. This encoding has also become the defacto format produced by modern Macs as well as iTunes on
    /// any platform. AIFF-C files produced by other applications typically have the file extension .aif and require
    /// looking at its header to detect the true format. The sowt encoding is the only encoding that SoX can handle
    /// with this format. Any private chunks are not supported.
    /// </summary>
    AIFC,
    /// <summary>
    /// Audio Interchage File Format. Used on old Apple Macs, Apple IIc/IIgs and SGI. SoX’s AIFF support does not
    /// include multiple audio chunks, or the 8SVX musical instrument description format. AIFF files are multimedia
    /// archives and can have multiple audio and picture chunks — you may need a separate archiver to work with them.
    /// With Mac OS X, AIFF has been superseded by CAF.
    /// </summary>
    AIFF,
    /// <summary>
    /// AIFF-C (not compressed), defined in DAVIC 1.4 Part 9 Annex B. Format based on AIFF that was created to allow
    /// handling compressed audio. It can also handle little endian uncompressed linear data that is often referred to
    /// as sowt encoding. This encoding has also become the defacto format produced by modern Macs as well as iTunes on
    /// any platform. AIFF-C files produced by other applications typically have the file extension .aif and require
    /// looking at its header to detect the true format. The sowt encoding is the only encoding that SoX can handle
    /// with this format. Any private chunks are not supported.
    /// </summary>
    AIFFC,
    /// <summary>
    /// Raw audio.
    /// </summary>
    AL,
    /// <summary>
    /// Ambisonic B‐Format. A specialisation of .wav with between 3 and 16 channels of audio for use with an Ambisonic
    /// decoder. It is up to the user to get the channels together in the right order and at the correct amplitude.
    /// </summary>
    AMB,
    /// <summary>
    /// Sun Microsystems AU file.
    /// </summary>
    AU,
    /// <summary>
    /// Audio Visual Research format; used by a number of commercial packages on the Mac.
    /// </summary>
    AVR,
    /// <summary>
    /// Apple’s Core Audio File format.
    /// </summary>
    CAF,
    /// <summary>
    /// ‘Red Book’ Compact Disc Digital Audio (raw audio). CDDA has two audio channels formatted as 16-bit signed
    /// integers (big endian)at a sample rate of 44.1 kHz. The number of (stereo) samples in each CDDA track is always
    /// a multiple of 588.
    /// </summary>
    CDDA,
    /// <summary>
    /// ‘Red Book’ Compact Disc Digital Audio (raw audio). CDDA has two audio channels formatted as 16-bit signed
    /// integers (big endian)at a sample rate of 44.1 kHz. The number of (stereo) samples in each CDDA track is always
    /// a multiple of 588.
    /// </summary>
    CDR,
    /// <summary>
    /// Headerless MIL Std 188 113 Continuously Variable Slope Delta modulation. A headerless format used to compress
    /// speech audio for applications such as voice mail. This format is sometimes used with bit-reversed samples.
    /// </summary>
    CVS,
    /// <summary>
    /// Headerless MIL Std 188 113 Continuously Variable Slope Delta modulation. A headerless format used to compress
    /// speech audio for applications such as voice mail. This format is sometimes used with bit-reversed samples.
    /// </summary>
    CVSD,
    /// <summary>
    /// Headerless Continuously Variable Slope Delta modulation (unfiltered). This is an alternative handler for CVSD
    /// that is unfiltered but can be used with any bit-rate.
    /// </summary>
    CVU,
    /// <summary>
    /// Text Data files. These files contain a textual representation of the sample data. There is one line at the
    /// beginning that contains the sample rate, and one line that contains the number of channels. Subsequent lines
    /// contain two or more numeric data intems: the time since the beginning of the first sample and the sample value
    /// for each channel.
    /// </summary>
    DAT,
    /// <summary>
    /// Self-describing variant of CVSD.
    /// </summary>
    DVMS,
    F32,
    F4,
    F64,
    F8,
    FAP,
    FLAC,
    FSSD,
    GSM,
    GSRT,
    HCOM,
    HTK,
    IMA,
    IRCAM,
    LA,
    LPC,
    LPC10,
    LU,
    MAUD,
    MP2,
    MP3,
    NIST,
    PRC,
    RAW,
    S1,
    S16,
    S2,
    S24,
    S3,
    S32,
    S4,
    S8,
    SB,
    SF,
    SL,
    SMP,
    SND,
    SNDR,
    SNDT,
    SOU,
    SOX,
    SPH,
    SW,
    TXW,
    U1,
    U16,
    U2,
    U24,
    U3,
    U32,
    U4,
    U8,
    UB,
    UL,
    UW,
    /// <summary>
    /// Self-describing variant of CVSD.
    /// </summary>
    VMS,
    VOC,
    VOX,
    WAV,
    WAVPCM,
    WVE,
    XA
  }

  /// <summary>
  /// Encoding type.
  /// </summary>
  public enum EncodingType
  {
    SignedInteger,
    UnsignedInteger,
    FloatingPoint,
    MuLaw,
    ALaw,
    ImaAdpcm,
    MsAdpcm,
    GsmFullRate
  }


  /// <summary>
  /// Byte order type.
  /// </summary>
  public enum ByteOrderType
  {
    /// <summary>
    /// Big endian byte order.
    /// </summary>
    BigEndian,
    /// <summary>
    /// Little endian byte order.
    /// </summary>
    LittleEndian,
    /// <summary>
    /// Swap the current byte order.
    /// </summary>
    Swap
  }


  /// <summary>
  /// SoX log message level.
  /// </summary>
  public enum LogLevelType
  {
    Error,
    Warning
  }
}
