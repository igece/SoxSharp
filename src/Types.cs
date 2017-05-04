namespace SoxSharp
{
  /// <summary>
  /// Audio file formats.
  /// </summary>
  public enum FileType
  {
    SVX,
    AIF,
    AIFC,
    AIFF,
    AIFFC,
    AL,
    AMB,
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
    BigEndian,
    LittleEndian,
    Swap
  }
}
