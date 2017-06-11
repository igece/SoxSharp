using System;


namespace SoxSharp
{
  /// <summary>
  /// Provides data for the <see cref="Sox.OnLogMessage"/> event. 
  /// </summary>
  public class LogMessageEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a <see cref="T:SoxSharp.LogMessageEventArgs"/> instance with the provided values.
    /// </summary>
    /// <param name="logLevel">Message severity.</param>
    /// <param name="message">Message text.</param>
    public LogMessageEventArgs(LogLevelType logLevel, string source, string message)
    {
      LogLevel = logLevel;
      Source = source;
      Message = message;
    }

    /// <summary>
    /// Message severity.
    /// </summary>
    public LogLevelType LogLevel { get; private set; }

    /// <summary>
    /// SoX logger module.
    /// </summary>
    public string Source { get; private set; }

    /// <summary>
    /// Message text.
    /// </summary>
    public string Message { get; private set; }
  }


  /// <summary>
  /// Provides data for the <see cref="Sox.OnProgress"/> event.
  /// </summary>
  public class ProgressEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes a <see cref="T:SoxSharp.ProgressEventArgs"/> instance with the provided values.
    /// </summary>
    /// <param name="progress">The actual progress value, from 0 to 100.</param>
    /// <param name="processed">File time that has been processed, based on file total duration.</param>
    /// <param name="remaining">File time pending to be processed, based on file total duration.</param>
    /// <param name="outputSize">Actual size of the generated output file.</param>
    public ProgressEventArgs(UInt16 progress, TimeSpan processed, TimeSpan remaining, UInt64 outputSize)
    {
      if (progress > 100)
        Progress = 100;
      else
        Progress = progress;

      Processed = processed;
      Remaining = remaining;
      OutputSize = outputSize;
      Abort = false;
    }

    /// <summary>
    /// The actual progress value, from 0 to 100.
    /// </summary>
    public UInt16 Progress { get; private set; }

    /// <summary>
    /// File time that has been processed, based on file total duration.
    /// </summary>
    public TimeSpan Processed { get; private set; }

    /// <summary>
    /// File time pending to be processed, based on file total duration.
    /// </summary>
    public TimeSpan Remaining { get; private set; }

    /// <summary>
    /// Actual size of the generated output file.
    /// </summary>
    public UInt64 OutputSize { get; private set; }

    /// <summary>
    /// Allows to cancel the current operation.
    /// </summary>
    /// <value><c>true</c> to cancel; otherwise, leave as <c>false</c>.</value>
    public bool Abort { get; set; }
  }
}
