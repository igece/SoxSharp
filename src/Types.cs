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
    AIF,
    AIFC,
    /// <summary>
    /// Audio Interchage File Format.
    /// </summary>
    AIFF,
    AIFFC,
    AL,
    /// <summary>
    /// Ambisonic B‐Format.
    /// </summary>
    AMB,
    /// <summary>
    /// Sun Microsystems AU file.
    /// </summary>
    AU,
    AVR,
    CDDA,
    CDR,
    CVS,
    CVSD,
    CVU,
    DAT,
    DVMS,
    F32,
    F4,
    F64,
    F8,
    FSSD,
    GSM,
    GSRT,
    HC,
    OM,
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
