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

    /// <summary>
    /// Ensoniq PARIS file format (little-endian).
    /// </summary>
    FAP,

    /// <summary>
    /// Xiph.org’s Free Lossless Audio Codec Compressed Audio. Open, patent-free codec designed for compressing music.
    /// SoX can read native FLAC files (.flac) but not Ogg FLAC files (.ogg).
    /// SoX can write native FLAC files according to a given or default compression level
    /// (see <see cref="OutputFormatOptions.Compression"/>). 8 is the default compression level and gives the best
    /// (but slowest) compression; 0 gives the least (but fastest) compression.
    /// </summary>
    FLAC,

    /// <summary>
    /// An alias for the .u8 format.
    /// </summary>
    FSSD,

    /// <summary>
    /// GSM 06.10 Lossy Speech Compression. A lossy format for compressing speech which is used in the Global Standard
    /// for Mobile telecommunications (GSM). It’s good for its purpose, shrinking audio data size, but it will introduce
    /// lots of noise when a given audio signal is encoded and decoded multiple times. This format is used by some voice
    /// mail applications. It is rather CPU intensive.
    /// </summary>
    GSM,

    /// <summary>
    /// Grandstream ringtone files. Whilst this file format can contain A-Law, μ-law, GSM, G.722, G.723, G.726, G.728, or iLBC
    /// encoded audio, SoX supports reading and writing only A-Law and μ-law.
    /// </summary>
    GSRT,

    /// <summary>
    /// Macintosh HCOM files. These are Mac FSSD files with Huffman compression.
    /// </summary>
    HCOM,

    /// <summary>
    /// Single channel 16-bit PCM format used by HTK, a toolkit for building Hidden Markov Model speech processing tools.
    /// </summary>
    HTK,

    /// <summary>
    /// A headerless file of IMA ADPCM audio data. IMA ADPCM claims 16-bit precision packed into only 4 bits, but in fact
    /// sounds no better than .vox.
    /// </summary>
    IMA,

    /// <summary>
    /// Another name for SF.
    /// </summary>
    IRCAM,

    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as inverse bit order 8-bit A-law.
    /// </summary>
    LA,

    /// <summary>
    /// LPC-10 is a compression scheme for speech developed in the United States. See http://www.arl.wustl.edu/˜jaf/lpc/
    /// for details. There is no associated file format, so SoX’s implementation is headerless.
    /// </summary>
    LPC,

    /// <summary>
    /// LPC-10 is a compression scheme for speech developed in the United States. See http://www.arl.wustl.edu/˜jaf/lpc/
    /// for details. There is no associated file format, so SoX’s implementation is headerless.
    /// </summary>
    LPC10,

    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as inverse bit order 8-bit μ-law.
    /// </summary>
    LU,

    /// <summary>
    /// An IFF-conforming audio file type, registered by MS MacroSystem Computer GmbH, published along with the ‘Toccata’
    /// soundcard on the Amiga. Allows 8-bit linear, 16-bit linear, A-Law, μ-law in mono and stereo.
    /// </summary>
    MAUD,

    MP2,

    /// <summary>
    /// MP3 compressed audio; MP3 (MPEG Layer 3) is a part of the MPEG standards for audio and video compression. It is a
    /// lossy compression format that achieves good compression rates with little quality loss.
    /// </summary>
    MP3,

    /// <summary>
    /// SPHERE (SPeech HEader Resources). Format defined by NIST (National Institute of Standards and Technology) and
    /// used with speech audio. SoX can read these files when they contain μ-law and PCM data. It will ignore any header
    /// information that says the data is compressed using shorten compression and will treat the data as either μ-law
    /// or PCM.
    /// </summary>
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

    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 1 byte signed integer PCM.
    /// Superseded by S8 type.
    /// </summary>
    SB,

    SF,

    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 4 bytes signed integer PCM.
    /// Superseded by S32 type.
    /// </summary>
    SL,

    SMP,
    SND,
    SNDR,
    SNDT,
    SOU,
    SOX,

    /// <summary>
    /// SPHERE (SPeech HEader Resources). Format defined by NIST (National Institute of Standards and Technology) and
    /// used with speech audio. SoX can read these files when they contain μ-law and PCM data. It will ignore any header
    /// information that says the data is compressed using shorten compression and will treat the data as either μ-law
    /// or PCM.
    /// </summary>
    SPH,

    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 2 bytes signed integer PCM.
    /// Superseded by S16 type.
    /// </summary>
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

    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 1 byte unsigned integer PCM.
    /// Superseded by U8 type.
    /// </summary>
    UB,

    /// <summary>
    /// Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 8-bit μ-law.
    /// </summary>
    UL,

    /// <summary>
    /// [DEPRECATED] Raw (headerless) audio file with a default sample rate of 8kHz and encoded as 2 bytes unsigned integer PCM.
    /// Superseded by S16 type.
    /// </summary>
    UW,

    /// <summary>
    /// Self-describing variant of CVSD.
    /// </summary>
    VMS,

    /// <summary>
    /// Sound Blaster VOC files. VOC files are multi-part and contain silence parts, looping, and different sample rates
    /// for different chunks. On input, the silence parts are filled out, loops are rejected, and sample data with a new
    /// sample rate is rejected. Silence with a different sample rate is generated appropriately. On output, silence is
    /// not detected, nor are impossible sample rates. SoX supports reading (but not writing) VOC files with multiple
    /// blocks, and files containing μ-law, A-law, and 2/3/4-bit ADPCM samples.
    /// </summary>
    VOC,

    /// <summary>
    /// A headerless file of Dialogic/OKI ADPCM audio data. This ADPCM data has 12-bit precision packed into only 4-bits.
    /// Some early Dialogic hardware does not always reset the ADPCM encoder at the start of each vox file. This can
    /// result in clipping and/or DC offset problems when it comes to decoding the audio. Whilst little can be done
    /// about the clipping, a DC offset can be removed by passing the decoded audio through a high-pass filter.
    /// </summary>
    VOX,

    /// <summary>
    /// Microsoft .WAV RIFF files. This is the native audio file format of Windows, and widely used for uncompressed
    /// audio. Normally .wav files have all formatting information in their headers, and so do not need any format
    /// options specified for an input file. If any are, they will override the file header, and you will be warned to
    /// this effect. Output format options will cause a format conversion, and the .wav will written appropriately.
    /// SoX can read and write linear PCM, floating point, μ-law, A-law, MS ADPCM, and IMA (or DVI) ADPCM encoded
    /// samples. WAV files can also contain audio encoded in many other ways (not currently supported with SoX)
    /// e.g. MP3; in some cases such a file can still be read by SoX by overriding the file type. Big endian versions
    /// of RIFF files, called RIFX, are also supported.
    /// </summary>
    WAV,

    /// <summary>
    /// A non-standard, but widely used, variant of .wav. Some applications cannot read a standard WAV file header for
    /// PCM-encoded data with sample-size greater than 16-bits or with more than two channels, but can read a non-
    /// standard WAV header. It is likely that such applications will eventually be updated to support the standard
    /// header, but in the mean time, this SoX format can be used to create files with the non-standard header that
    /// should work with these applications. Note that SoX will automatically detect and read WAV files with the
    /// non-standard header.
    /// </summary>
    WAVPCM,

    /// <summary>
    /// Psion 8-bit A-law. Used on Psion SIBO PDAs (Series 3 and similar). This format is deprecated in SoX, but will
    /// continue to be used in libsndfile.
    /// </summary>
    WVE,

    /// <summary>
    /// Maxis XA files. These are 16-bit ADPCM audio files used by Maxis games. Writing .xa files is currently not
    /// supported.
    /// </summary>
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
