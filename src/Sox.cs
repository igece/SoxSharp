using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SoxSharp.Effects;


namespace SoxSharp
{
  /// <summary>
  /// Encapsulates all the information needed to launch
  /// </summary>
  public class Sox : IDisposable
  {
    /// <summary>
    /// Provides updated progress status while <see cref="Sox.Process"/> is being executed.
    /// </summary>
    public event EventHandler<ProgressEventArgs> OnProgress = null;

    /// <summary>
    /// Occurs when SoX sends a warning or error message.
    /// </summary>
    public event EventHandler<LogMessageEventArgs> OnLogMessage = null;

    /// <summary>
    /// Location of the SoX executable to be used by the library.
    /// </summary>
    /// <value>The executable path.</value>
    public string Path { get; set; }

    /// <summary>
    /// Size of all processing buffers (default is 8192).
    /// </summary>
    public uint? Buffer { get; set; }

    /// <summary>
    /// Enable parallel effects channels processing (where available).
    /// </summary>
    public bool? Multithreaded { get; set; }

    /// <summary>
    /// Input format options.
    /// </summary>
    public InputFormatOptions Input { get; private set; }

    /// <summary>
    /// Output format options.
    /// </summary>
    public OutputFormatOptions Output { get; private set; }

    /// <summary>
    /// Filters to be applied.
    /// </summary>
    /// <value>The filters.</value>
    public List<IBaseEffect> Effects { get; private set; }

    /// <summary>
    /// Custom global arguments.
    /// </summary>
    public string CustomArgs { get; set; }


    private SoxProcess soxProcess_ = null;
    private bool disposed_ = false;


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Sox"/> class.
    /// </summary>
    /// <param name="path">Location of the SoX executable to be used by the library.</param>
    public Sox(string path)
    {
      Input = new InputFormatOptions();
      Output = new OutputFormatOptions();
      Effects = new List<IBaseEffect>();
      Path = path;
    }


    /// <summary>
    /// Releases all resource used by the <see cref="T:SoxSharp.Sox"/> object.
    /// </summary>
    /// <remarks>Call <see cref="T:Sox.Dispose"/> when you are finished using the <see cref="T:SoxSharp.Sox"/> instance. This
    /// <see cref="T:Sox.Dispose"/> method leaves the <see cref="T:SoxSharp.Sox"/> instance in an unusable state. After calling
    /// it, you must release all references to the instance so the garbage
    /// collector can reclaim the memory that it was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    /// <summary>
    /// Gets information about the given file. 
    /// </summary>
    /// <returns>File information as a <see cref="SoxSharp.AudioInfo"/> instance.</returns>
    /// <param name="inputFile">Input file.</param>
    public AudioInfo GetInfo(string inputFile)
    {
      if (!File.Exists(inputFile))
        throw new FileNotFoundException("File not found: " + inputFile);

      soxProcess_ = SoxProcess.Create(Path);

      try
      {
        soxProcess_.StartInfo.RedirectStandardOutput = true;
        soxProcess_.StartInfo.Arguments = "--info " + inputFile;
        soxProcess_.Start();

        string output = soxProcess_.StandardOutput.ReadToEnd();

        if (soxProcess_.WaitForExit(10000) == false)
          throw new TimeoutException("SoX response timeout");

        CheckForLogMessage(output);
        CheckExitCode(soxProcess_.ExitCode);

        if (output != null)
        {
          Match matchInfo = SoxProcess.InfoRegex.Match(output);

          if (matchInfo.Success)
          {
            try
            {
              UInt16 channels = Convert.ToUInt16(double.Parse(matchInfo.Groups[1].Value, CultureInfo.InvariantCulture));
              UInt32 sampleRate = Convert.ToUInt32(double.Parse(matchInfo.Groups[2].Value, CultureInfo.InvariantCulture));
              UInt16 sampleSize = Convert.ToUInt16(double.Parse(new string(matchInfo.Groups[3].Value.Where(Char.IsDigit).ToArray()), CultureInfo.InvariantCulture));
              TimeSpan duration = TimeSpan.ParseExact(matchInfo.Groups[4].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
              UInt64 size = FormattedSize.ToUInt64(matchInfo.Groups[5].Value);
              UInt32 bitRate = FormattedSize.ToUInt32(matchInfo.Groups[6].Value);
              string encoding = matchInfo.Groups[7].Value;

              return new AudioInfo(channels, sampleRate, sampleSize, duration, size, bitRate, encoding);
            }

            catch (Exception ex)
            {
              throw new SoxException("Cannot parse SoX output", ex);
            }
          }
        }

        throw new SoxException("Unexpected output from SoX");
      }

      finally
      {
        if (soxProcess_ != null)
        {
          soxProcess_.Dispose();
          soxProcess_ = null;
        }
      }
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <returns>Exit code returned by SoX.</returns>
    /// <param name="inputFile">Audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public int Process(string inputFile, string outputFile)
    {
      soxProcess_ = SoxProcess.Create(Path);

      try
      {
        soxProcess_.ErrorDataReceived += ((sender, received) =>
        {
          if (received.Data != null)
          {
            if (OnProgress != null)
            {
              Match matchProgress = SoxProcess.ProgressRegex.Match(received.Data);

              if (matchProgress.Success)
              {
                try
                {
                  UInt16 progress = Convert.ToUInt16(double.Parse(matchProgress.Groups[1].Value, CultureInfo.InvariantCulture));
                  TimeSpan processed = TimeSpan.ParseExact(matchProgress.Groups[2].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                  TimeSpan remaining = TimeSpan.ParseExact(matchProgress.Groups[3].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                  UInt64 outputSize = FormattedSize.ToUInt64(matchProgress.Groups[4].Value);

                  ProgressEventArgs eventArgs = new ProgressEventArgs(progress, processed, remaining, outputSize);
                  OnProgress(sender, eventArgs);

                  if (eventArgs.Abort)
                    Abort();

                  return;
                }

                catch (OverflowException)
                {
                  // SoX v14.3.1 (at least) sometimes report invalid time values (i.e. 06:31:60.00).
                  // Just ignore this progress update.
                  return;
                }

                catch (Exception ex)
                {
                  throw new SoxException("Unexpected output from SoX", ex);
                }
              }
            }

            if (CheckForLogMessage(received.Data))
              return;
          }
        });

        List<string> args = new List<string>();

        // Global options.

        if (Buffer.HasValue)
          args.Add("--buffer " + Buffer.Value);

        if (Multithreaded.HasValue)
          args.Add(Multithreaded.Value ? "--multi-threaded" : "--single-threaded");

        if (!String.IsNullOrEmpty(CustomArgs))
          args.Add(CustomArgs);

        args.Add("--show-progress");

        // Input options.
        args.Add(Input.ToString());
        args.Add(inputFile);

        // Output options.
        args.Add(Output.ToString());
        args.Add(outputFile);

        // Effects.
        foreach (IBaseEffect effect in Effects)
          args.Add(effect.ToString());

        soxProcess_.StartInfo.Arguments = String.Join(" ", args);

        try
        {
          soxProcess_.Start();
          soxProcess_.BeginErrorReadLine();
          soxProcess_.WaitForExit();

          if (soxProcess_ != null)
            return soxProcess_.ExitCode;

          return -1;
        }

        catch (Exception ex)
        {
          throw new SoxException("Cannot spawn SoX process", ex);
        }
      }

      finally
      {
        if (soxProcess_ != null)
        {
          soxProcess_.Dispose();
          soxProcess_ = null;
        }
      }
    }


    /// <summary>
    /// Kills the SoX process.
    /// </summary>
    public void Abort()
    {
      if ((soxProcess_ != null) && (soxProcess_.Id != -1))
      {
        try
        {
          soxProcess_.Kill();
        }

        finally
        {
          soxProcess_.Dispose();
          soxProcess_ = null;
        }
      }
    }


    protected bool CheckForLogMessage(string data)
    {
      if (string.IsNullOrEmpty(data))
        return false;

      Match logMatch = SoxProcess.LogRegex.Match(data);

      if (logMatch.Success)
      {
        try
        {
          string logLevel = logMatch.Groups[1].Value;
          string message = logMatch.Groups[2].Value;

          if (OnLogMessage != null)
          {
            if ("WARN".Equals(logLevel))
              OnLogMessage(this, new LogMessageEventArgs(LogLevelType.Warning, message));
            else if ("FAIL".Equals(logLevel))
              OnLogMessage(this, new LogMessageEventArgs(LogLevelType.Error, message));
          }

          return true;
        }

        catch (Exception)
        {
          return false;
        }
      }

      return false;
    }


    protected void CheckExitCode(int exitCode)
    {
      switch (exitCode)
      {
        case 1:
          throw new SoxException("SoX did not recognized some command-line parameters");

        case 2:
          throw new SoxException("SoX returned an error while processing");
      }
    }


    private void Dispose(bool disposing)
    {
      if (disposed_)
        return;

      if ((soxProcess_ != null) && (!soxProcess_.HasExited))
        Abort();

      disposed_ = true;
    }
  }
}
