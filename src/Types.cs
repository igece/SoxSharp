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
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8-bit A-law.
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
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 32-bit floating point PCM.
    /// </summary>
    F32,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 4 bytes floating point PCM.
    /// Superseded by F32 type.
    /// </summary>
    F4,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 64-bit floating point PCM.
    /// </summary>
    F64,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8 bytes floating point PCM.
    /// Superseded by F64 type.
    /// </summary>
    F8,
    FAP,
    /// <summary>
    /// Xiph.org’s Free Lossless Audio Codec Compressed Audio. Open, patent-free codec designed for compressing music.
    /// SoX can read native FLAC files (.flac) but not Ogg FLAC files (.ogg).
    /// SoX can write native FLAC files according to a given or default compression level
    /// (see <see cref="OutputFormatOptions.Compression"/>). 8 is the default compression level and gives the best
    /// (but slowest) compression; 0 gives the least (but fastest) compression.
    /// </summary>
    FLAC,
    FSSD,
    GSM,
    GSRT,
    HCOM,
    HTK,
    IMA,
    IRCAM,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as inverse bit order 8-bit A-law.
    /// </summary>
    LA,
    LPC,
    LPC10,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as inverse bit order 8-bit μ-law.
    /// </summary>
    LU,
    MAUD,
    MP2,
    MP3,
    NIST,
    PRC,
    /// <summary>
    /// Raw (headerless) audio file.
    /// </summary>
    RAW,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 1 byte signed integer PCM.
    /// Superseded by S8 type.
    /// /// </summary>
    S1,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 16-bit signed integer PCM.
    /// </summary>
    S16,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 2 bytes signed integer PCM.
    /// Superseded by S16 type.
    /// </summary>
    S2,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 24-bit signed integer PCM.
    /// </summary>
    S24,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 3 bytes signed integer PCM.
    /// Superseded by S24 type.
    /// </summary>
    S3,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 32-bit signed integer PCM.
    /// </summary>
    S32,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 4 bytes signed integer PCM.
    /// Superseded by S32 type.
    /// </summary>
    S4,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8-bit signed integer PCM.
    /// </summary>
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
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 1 byte unsigned integer PCM.
    /// Superseded by U8 type.
    /// </summary>
    U1,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 16-bit unsigned integer PCM.
    /// </summary>
    U16,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 2 bytes unsigned integer PCM.
    /// Superseded by U16 type.
    /// </summary>
    U2,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 24-bit unsigned integer PCM.
    /// </summary>
    U24,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 3 bytes unsigned integer PCM.
    /// Superseded by U24 type.
    /// </summary>
    U3,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 32-bit unsigned integer PCM.
    /// </summary>
    U32,
    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 4 bytes unsigned integer PCM.
    /// Superseded by U32 type.
    /// </summary>
    U4,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8-bit unsigned integer PCM.
    /// </summary>
    U8,
    UB,
    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8-bit μ-law.
    /// </summary>
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
    /// <summary>
    /// Error message.
    /// </summary>
    Error,
    /// <summary>
    /// Warning message.
    /// </summary>
    Warning
  }
}
