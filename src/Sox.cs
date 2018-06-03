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

    /// <summary>
    /// Gets the full command line of the last call to SoX.
    /// </summary>
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

        if (inputFile.Contains(" "))
        {
          if ((Environment.OSVersion.Platform == PlatformID.Win32NT) ||
              (Environment.OSVersion.Platform == PlatformID.Win32Windows) ||
              (Environment.OSVersion.Platform == PlatformID.Win32S) ||
              (Environment.OSVersion.Platform == PlatformID.WinCE))
            soxProcess_.StartInfo.Arguments = "--info \"" + inputFile + "\"";
          else
            soxProcess_.StartInfo.Arguments = "--info '" + inputFile + "'";
        }
        else
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
              TimeSpan duration = Utils.TimeSpanFromString(matchInfo.Groups[4].Value);
              UInt64 size = FormattedSize.ToUInt64(matchInfo.Groups[5].Value);
              UInt32 bitRate = FormattedSize.ToUInt32(matchInfo.Groups[6].Value);
              string encoding = matchInfo.Groups[7].Value;

              return new AudioInfo(channels, sampleRate, sampleSize, duration, size, bitRate, encoding);
            }

            catch (Exception ex)
            {
              throw new SoxUnexpectedOutputException(output, ex);
            }
          }
        }

        throw new SoxUnexpectedOutputException(output != null ? output : "No output received");
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
    /// Spawns a new SoX process using the specified options in this instance and record audio the specified file.
    /// </summary>
    /// <param name="outputFile">Audio file to be recorded.</param>
    public void Record(string outputFile)
    {
      Process("--default-device", outputFile);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified file.
    /// </summary>
    /// <param name="inputFile">Audio file to be played.</param>
    public void Play(string inputFile)
    {
      Process(new InputFile[] { new InputFile(inputFile) }, "--default-device");
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified file.
    /// </summary>
    /// <param name="inputFile">Audio file to be played.</param>
    public void Play(InputFile inputFile)
    {
      Process(new InputFile[] { inputFile }, "--default-device");
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified files.
    /// </summary>
    /// <param name="inputFiles">Audio files to be played.</param>
    public void Play(string[] inputFiles)
    {
      var inputs = new List<InputFile>(inputFiles.Length);

      foreach (var inputFile in inputFiles)
        inputs.Add(new InputFile(inputFile));

      Process(inputs.ToArray(), "--default-device");
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified files.
    /// </summary>
    /// <param name="inputFiles">Audio files to be played.</param>
    /// <param name="combination">How to combine the input files.</param>
    public void Play(string[] inputFiles, CombinationType combination)
    {
      var inputs = new List<InputFile>(inputFiles.Length);

      foreach (var inputFile in inputFiles)
        inputs.Add(new InputFile(inputFile));

      Process(inputs.ToArray(), "--default-device", combination);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified files.
    /// </summary>
    /// <param name="inputFiles">Audio files to be played.</param>
    public void Play(InputFile[] inputFiles)
    {
      Process(inputFiles, "--default-device");
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance and plays the specified files.
    /// </summary>
    /// <param name="inputFiles">Audio files to be played.</param>
    /// <param name="combination">How to combine the input files.</param>
    public void Play(InputFile[] inputFiles, CombinationType combination)
    {
      Process(inputFiles, "--default-device", combination);
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
    /// <param name="combination">How to combine the input files.</param>
    public void Process(string inputFile1, string inputFile2, string outputFile, CombinationType combination)
    {
      Process(new InputFile[] { new InputFile(inputFile1), new InputFile(inputFile2) }, outputFile, combination);
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
    /// <param name="inputFile1">First audio file to be processed.</param>
    /// <param name="inputFile2">Second audio file to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    /// <param name="combination">How to combine the input files.</param>
    public void Process(InputFile inputFile1, InputFile inputFile2, string outputFile, CombinationType combination)
    {
      Process(new InputFile[] { inputFile1, inputFile2 }, outputFile, combination);
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
    /// <param name="combination">How to combine the input files.</param>
    public void Process(string[] inputFiles, string outputFile, CombinationType combination)
    {
      var inputs = new List<InputFile>(inputFiles.Length);

      foreach (var inputFile in inputFiles)
        inputs.Add(new InputFile(inputFile));

      Process(inputs.ToArray(), outputFile, combination);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFiles">Audio files to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    public void Process(InputFile[] inputFiles, string outputFile)
    {
      Process(inputFiles, outputFile, CombinationType.Default);
    }


    /// <summary>
    /// Spawns a new SoX process using the specified options in this instance.
    /// </summary>
    /// <param name="inputFiles">Audio files to be processed.</param>
    /// <param name="outputFile">Output file.</param>
    /// <param name="combination">How to combine the input files.</param>
    public void Process(InputFile[] inputFiles, string outputFile, CombinationType combination)
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

        switch (combination)
        {
          case CombinationType.Concatenate:
            args.Add("--combine concatenate");
            break;

          case CombinationType.Merge:
            args.Add("--combine merge");
            break;

          case CombinationType.Mix:
            args.Add("--combine mix");
            break;

          case CombinationType.MixPower:
            args.Add("--combine mix-power");
            break;

          case CombinationType.Multiply:
            args.Add("--combine multiply");
            break;

          case CombinationType.Sequence:
            args.Add("--combine sequence");
            break;

          default:
            // Do nothing.
            break;
        }

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

        if (!string.IsNullOrEmpty(outputFile))
        {
          if (outputFile.Contains(" "))
          {
            if ((Environment.OSVersion.Platform == PlatformID.Win32NT) ||
                (Environment.OSVersion.Platform == PlatformID.Win32Windows) ||
                (Environment.OSVersion.Platform == PlatformID.Win32S) ||
                (Environment.OSVersion.Platform == PlatformID.WinCE))
              args.Add("\"" + outputFile + "\"");
            else
              args.Add("'" + outputFile + "'");
          }
          else
            args.Add(outputFile);
        }
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
              TimeSpan processed = Utils.TimeSpanFromString(matchProgress.Groups[2].Value);
              TimeSpan remaining = Utils.TimeSpanFromString(matchProgress.Groups[3].Value);
              UInt64 outputSize = FormattedSize.ToUInt64(matchProgress.Groups[4].Value);

              ProgressEventArgs eventArgs = new ProgressEventArgs(progress, processed, remaining, outputSize);
              OnProgress(sender, eventArgs);

              if (eventArgs.Abort)
                Abort();

              return;
            }

            catch (Exception ex)
            {
              throw new SoxUnexpectedOutputException(received.Data, ex);
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


    private void CheckForLogMessage(string data)
    {
      if (string.IsNullOrEmpty(data))
        return;

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
      }
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
