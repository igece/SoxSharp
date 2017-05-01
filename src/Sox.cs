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
  /// Encapsulates all 
  /// </summary>
  public class Sox : IDisposable
  {
    /// <summary>
    /// Bla, bla bla...
    /// </summary>
    public event EventHandler<ProgressEventArgs> OnProgress = null;


    /// <summary>
    /// Size of all processing buffers (default is 8192).
    /// </summary>
    public UInt16? Buffer { get; set; }

    /// <summary>
    /// Enable parallel effects channels processing (where available).
    /// </summary>
    public bool? Multithreaded { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public InputFormatOptions Input { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public OutputFormatOptions Output { get; protected set; }


    private bool disposed_ = false;

    private string soxExecutable_;

    private Regex infoRegex_ = new Regex(@"Input File\s*: .+\r\nChannels\s*: (\d+)\r\nSample Rate\s*: (\d+)\r\nPrecision\s*: ([\s\S]+?)\r\nDuration\s*: (\d{2}:\d{2}:\d{2}\.?\d{2}?)[\s\S]+?\r\nFile Size\s*: (\d+\.?\d{0,2}?[k|M|G]?)\r\nBit Rate\s*: (\d+\.?\d{0,2}?k?)\r\nSample Encoding\s*: (.+)");
    private Regex progressRegex_ = new Regex(@"In:(\d{1,3}\.?\d{0,2})%\s+(\d{2}:\d{2}:\d{2}\.?\d{0,2})\s+\[(\d{2}:\d{2}:\d{2}\.?\d{0,2})\]\s+Out:(\d+\.?\d{0,2}[k|M|G]?)");
    

    public Sox()
    {
      Input = new InputFormatOptions();
      Output = new OutputFormatOptions();

      soxExecutable_ = Path.Combine(Path.GetTempPath(), "sox.exe");

      byte[] soxHash = new byte[] { 0x05, 0xd5, 0x00, 0x8a, 0x50, 0x56, 0xe2, 0x8e, 0x45, 0xad, 0x1e, 0xb7, 0xd7, 0xbe, 0x9f, 0x03 };

      if (!File.Exists(soxExecutable_))
        File.WriteAllBytes(soxExecutable_, SoxSharp.Resources.sox);
      else
      {
        using (MD5 md5 = MD5.Create())
        {
          using (FileStream stream = File.OpenRead(soxExecutable_))
          {
            byte[] hash = md5.ComputeHash(stream);
            
            if (!hash.SequenceEqual(soxHash))
              File.WriteAllBytes(soxExecutable_, SoxSharp.Resources.sox);
          }
        }
      }
    }


    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    public FileInfo GetInfo(string inputFile)
    {
      if (!File.Exists(inputFile))
        throw new FileNotFoundException("File not found: " + inputFile);

      using (Process soxCmd = CreateSoxProcess())
      {
        soxCmd.StartInfo.RedirectStandardOutput = true;
        soxCmd.StartInfo.Arguments = "--info " + inputFile;
        soxCmd.Start();
        
        string result = soxCmd.StandardOutput.ReadToEnd();
        soxCmd.WaitForExit();

        if (result != null)
        {
          Match match = infoRegex_.Match(result);

          if (match.Success)
          {
            try
            {
              UInt16 channels = Convert.ToUInt16(double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
              UInt32 sampleRate = Convert.ToUInt32(double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture));
              UInt16 sampleSize = Convert.ToUInt16(double.Parse(new string(match.Groups[3].Value.Where(Char.IsDigit).ToArray()), CultureInfo.InvariantCulture));
              TimeSpan duration = TimeSpan.ParseExact(match.Groups[4].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
              UInt64 size = FormattedSize.ToUInt64(match.Groups[5].Value);
              UInt32 bitRate = FormattedSize.ToUInt32(match.Groups[6].Value);
              string encoding = match.Groups[7].Value;

              return new FileInfo(channels, sampleRate, sampleSize, duration, size, bitRate, encoding);
            }

            catch (Exception ex)
            {
              throw new Exception("Unexpected output from SoX", ex);
            }
          }
          else
          {
            throw new Exception(result);
          }
        }

        throw new Exception("Unexpected output from SoX");
      }
    }


    public int Process(string inputFile, string outputFile)
    {
      using (Process soxCmd = CreateSoxProcess())
      {       
        soxCmd.ErrorDataReceived += ((sender, received) =>
        {
          if (received.Data != null)
          {
            if (OnProgress != null)
            {
              Match match = progressRegex_.Match(received.Data);

              if (match.Success)
              {
                try
                {
                  UInt16 progress = Convert.ToUInt16(double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
                  TimeSpan processed = TimeSpan.ParseExact(match.Groups[2].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                  TimeSpan remaining = TimeSpan.ParseExact(match.Groups[3].Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);
                  UInt64 outputSize = FormattedSize.ToUInt64(match.Groups[4].Value);
                  
                  OnProgress(sender, new ProgressEventArgs(progress, processed, remaining, outputSize));
                }

                catch (Exception ex)
                {
                  throw new Exception("Unexpected output from SoX", ex);
                }                
              }
            }
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

        soxCmd.StartInfo.Arguments = String.Join(" ", args);
        soxCmd.Start();
        soxCmd.BeginErrorReadLine();
        soxCmd.WaitForExit();

        return soxCmd.ExitCode;
      }
    }


    private void Dispose(bool disposing)
    {
      if (disposed_)
        return;

      disposed_ = true;
    }


    private Process CreateSoxProcess()
    {
      Process soxProc = new Process();

      soxProc.StartInfo.FileName = soxExecutable_;
      soxProc.StartInfo.ErrorDialog = false;
      soxProc.StartInfo.CreateNoWindow = true;
      soxProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      soxProc.StartInfo.UseShellExecute = false;
      soxProc.StartInfo.RedirectStandardError = true;
      soxProc.EnableRaisingEvents = true;

      return soxProc;
    }
  }
}
