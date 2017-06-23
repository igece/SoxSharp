using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SoxSharp.Effects;
using SoxSharp.Exceptions;


namespace SoxSharp
{
  /// <summary>
  /// Encapsulates all the information needed to launch SoX and handle its output.
  /// </summary>
  public class Sox : IDisposable
  {
    /// <summary>
    /// Provides updated progress status while SoX is being executed.
    /// </summary>
    public event EventHandler<ProgressEventArgs> OnProgress = null;

    /// <summary>
    /// Occurs when SoX generates any non-FAIL log message.
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
    /// Output format options.
    /// </summary>
    public OutputFormatOptions Output { get; private set; }

    /// <summary>
    /// Effects to be applied.
    /// </summary>
    public List<IBaseEffect> Effects { get; private set; }

    /// <summary>
    /// Custom global arguments.
    /// </summary>
    public string CustomArgs { get; set; }

    /// <summary>
    /// Custom effects. Add here the command line arguments for any effect not currently implemented in SoXSharp.
    /// </summary>
    public string CustomEffects { get; set; }


    public string LastCommand { get; private set; }


    private SoxProcess soxProcess_ = null;
    private string lastError_ = null;
    private string lastErrorSource_ = null;
    private bool disposed_ = false;


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Sox"/> class.
    /// </summary>
    /// <param name="path">Location of the SoX executable to be used by the library.</param>
    public Sox(string path)
    {
      Output = new OutputFormatOptions();
      Effects = new List<IBaseEffect>();
      Path = path;
    }


    ~Sox()
    {
      Dispose(false);
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

      lastError_ = null;
      lastErrorSource_ = null;

      try
      {
        soxProcess_.StartInfo.RedirectStandardOutput = true;
        soxProcess_.StartInfo.Arguments = "--info " + inputFile;
        soxProcess_.Start();

        LastCommand = Path + " " + soxProcess_.StartInfo.Arguments;

        string output = soxProcess_.StandardOutput.ReadToEnd();

        if (String.IsNullOrEmpty(output))
          output = soxProcess_.StandardError.ReadToEnd();

        if (soxProcess_.WaitForExit(10000) == false)
          throw new TimeoutException("SoX response timeout");

        CheckForLogMessage(output);

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
    /// <param name="inputFile">Audio file to be processed.</param>
    public void Process(string inputFile)
    {
      Process(new InputFile[] { new InputFile(inputFile) }, null);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFile">Audio file to be processed.</param>
    public void Process(InputFile inputFile)
    {
      Process(new InputFile[] { inputFile }, null);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFile">Audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(string inputFile, string outputFile)
    {
      Process(new InputFile[] { new InputFile(inputFile) }, outputFile);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFile">Audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(InputFile inputFile, string outputFile)
    {
      Process(new InputFile[] { inputFile }, outputFile);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFile1">First audio file to be processed.</param>
    /// <param name="inputFile2">Second audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(string inputFile1, string inputFile2, string outputFile)
    {
      Process(new InputFile[] { new InputFile(inputFile1), new InputFile(inputFile2) }, outputFile);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFile1">First audio file to be processed.</param>
    /// <param name="inputFile2">Second audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(InputFile inputFile1, InputFile inputFile2, string outputFile)
    {
      Process(new InputFile[] { inputFile1, inputFile2 }, outputFile);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFiles">Audio files to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(string[] inputFiles, string outputFile)
    {
      var inputs = new List<InputFile>(inputFiles.Length);

      foreach (var inputFile in inputFiles)
        inputs.Add(new InputFile(inputFile));

      Process(inputs.ToArray(), outputFile);
    }

    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFiles">Audio files to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(InputFile[] inputFiles, string outputFile)
    {
      soxProcess_ = SoxProcess.Create(Path);

      lastError_ = null;
      lastErrorSource_ = null;

      try
      {
        soxProcess_.ErrorDataReceived += OnSoxProcessOutputReceived;
        soxProcess_.OutputDataReceived += OnSoxProcessOutputReceived;

        List<string> args = new List<string>();

        // Global options.

        if (Buffer.HasValue)
          args.Add("--buffer " + Buffer.Value);

        if (Multithreaded.HasValue)
          args.Add(Multithreaded.Value ? "--multi-threaded" : "--single-threaded");

        if (!String.IsNullOrEmpty(CustomArgs))
          args.Add(CustomArgs);

        args.Add("--show-progress");

        // Input options and files.

        if ((inputFiles != null) && (inputFiles.Length > 0))
        {
          foreach (InputFile inputFile in inputFiles)
            args.Add(inputFile.ToString());
        }
        else
          args.Add("--null");

        // Output options and file.

        args.Add(Output.ToString());

        if (outputFile != null)
          args.Add(outputFile);
        else
          args.Add("--null");

        // Effects.
        foreach (IBaseEffect effect in Effects)
          args.Add(effect.ToString());

        // Custom effects.
        args.Add(CustomEffects);

        soxProcess_.StartInfo.Arguments = String.Join(" ", args);
        LastCommand = Path + " " + soxProcess_.StartInfo.Arguments;

        try
        {
          soxProcess_.Start();
          soxProcess_.BeginOutputReadLine();
          soxProcess_.BeginErrorReadLine();
          soxProcess_.WaitForExit();
        }

        catch (Exception ex)
        {
          throw new SoxException("Cannot spawn SoX process", ex);
        }

        if (!String.IsNullOrEmpty(lastError_))
        {
          if (String.IsNullOrEmpty(lastErrorSource_))
            throw new SoxException(lastError_);

          switch (lastErrorSource_)
          {
            case "getopt":
              throw new SoxException("Invalid parameter: " + lastError_);

            default:
              throw new SoxException("Processing error: " + lastError_);
          }
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


    private void OnSoxProcessOutputReceived(object sender, DataReceivedEventArgs received)
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

        CheckForLogMessage(received.Data);
      }
    }


    /// <summary>
    /// Kills the SoX process.
    /// </summary>
    public void Abort()
    {
      if ((soxProcess_ != null) && !soxProcess_.HasExited)
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
        string logLevel = logMatch.Groups[1].Value;
        string source = logMatch.Groups[2].Value;
        string message = logMatch.Groups[3].Value;

        if ("DBUG".Equals(logLevel) && (OnLogMessage != null))
          OnLogMessage(this, new LogMessageEventArgs(LogLevelType.Debug, source, message));

        if ("INFO".Equals(logLevel) && (OnLogMessage != null))
          OnLogMessage(this, new LogMessageEventArgs(LogLevelType.Info, source, message));

        if ("WARN".Equals(logLevel) && (OnLogMessage != null))
          OnLogMessage(this, new LogMessageEventArgs(LogLevelType.Warning, source, message));

        else if ("FAIL".Equals(logLevel))
        {
          if (String.IsNullOrEmpty(lastError_))
            lastError_ = message;

          if (String.IsNullOrEmpty(lastErrorSource_))
            lastErrorSource_ = source;
        }

        return true;
      }

      return false;
    }


    protected virtual void Dispose(bool disposing)
    {
      if (disposed_)
        return;

      if (disposing)
        Abort();

      disposed_ = true;
    }
  }
}
