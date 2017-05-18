using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


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
    /// Location of the SoX binary to be used by the library. If no path is specified (null or empty string) AND
    /// only on Windows platforms SoxSharp will use a binary copy included within the library (version 14.4.2).
    /// On MacOSX and Linux systems, this property must be correctly set before any call to the <see cref="Process"/> method.
    /// </summary>
    /// <value>The binary path.</value>
    public string Path { get; set; }

    /// <summary>
    /// Size of all processing buffers (default is 8192).
    /// </summary>
    public UInt16? Buffer { get; set; }

    /// <summary>
    /// Enable parallel effects channels processing (where available).
    /// </summary>
    public bool? Multithreaded { get; set; }

    /// <summary>
    /// Input format options.
    /// </summary>
    public InputFormatOptions Input { get; protected set; }

    /// <summary>
    /// Output format options.
    /// </summary>
    public OutputFormatOptions Output { get; protected set; }

    private SoxProcess soxProcess_ = null;
    private bool disposed_ = false;


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.Sox"/> class.
    /// </summary>
    public Sox()
    {
      Input = new InputFormatOptions();
      Output = new OutputFormatOptions();
    }


    public Sox(string path)
    {
      Input = new InputFormatOptions();
      Output = new OutputFormatOptions();
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
    /// <returns>File information as a <see cref="SoxSharp.FileInfo"/> instance.</returns>
    /// <param name="inputFile">Input file.</param>
    public FileInfo GetInfo(string inputFile)
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
        
        if (output != null)
        {
          Match matchInfo = soxProcess_.Regex.FileInfo.Match(output);

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

              return new FileInfo(channels, sampleRate, sampleSize, duration, size, bitRate, encoding);
            }

            catch (Exception ex)
            {
              throw new SoxException("Unexpected output from SoX", ex);
            }
          }

          if (CheckForLogMessage(output))
            return null;
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
              Match matchProgress = soxProcess_.Regex.Progress.Match(received.Data);

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

        args.Add("--show-progress");

        // Input options.
        args.Add(Input.ToString());
        args.Add(inputFile);

        // Output options.
        args.Add(Output.ToString());
        args.Add(outputFile);

        soxProcess_.StartInfo.Arguments = String.Join(" ", args);

        try
        {
          soxProcess_.Start();
          soxProcess_.BeginErrorReadLine();
          soxProcess_.WaitForExit();

          if (soxProcess_ != null)
            return soxProcess_.ExitCode;
          else
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
      Match logMatch = soxProcess_.Regex.Log.Match(data);

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
