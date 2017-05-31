using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using SoxSharp.Exceptions;

namespace SoxSharp
{
  sealed class SoxProcess : Process
  {
    public static readonly Regex InfoRegex = new Regex(@"Input File\s*: .+\r?\nChannels\s*: (\d+)\r?\nSample Rate\s*: (\d+)\r?\nPrecision\s*: ([\s\S]+?)\r?\nDuration\s*: (\d{2}:\d{2}:\d{2}\.?\d{2}?)[\s\S]+?\r?\nFile Size\s*: (\d+\.?\d{0,2}?[k|M|G]?)\r?\nBit Rate\s*: (\d+\.?\d{0,2}?[k|M|G]?)\r?\nSample Encoding\s*: (.+)");
    public static readonly Regex ProgressRegex = new Regex(@"In:(\d{1,3}\.?\d{0,2})%\s+(\d{2}:\d{2}:\d{2}\.?\d{0,2})\s+\[(\d{2}:\d{2}:\d{2}\.?\d{0,2})\]\s+Out:(\d+\.?\d{0,2}[k|M|G]?)");
    public static readonly Regex LogRegex = new Regex(@"(FAIL|WARN)\s(.+)");


    private SoxProcess()
    : base()
    {
      StartInfo.ErrorDialog = false;
      StartInfo.CreateNoWindow = true;
      StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      StartInfo.UseShellExecute = false;
      StartInfo.RedirectStandardOutput = true;
      StartInfo.RedirectStandardError = true;
      EnableRaisingEvents = true;
    }


    /// <summary>
    /// Create a new <see cref="T:SoxSharp.SoxProcess"/> instance prepared to run SoX.
    /// </summary>
    /// <returns>The SoX process instance.</returns>
    public static SoxProcess Create(string path)
    {
      string soxExecutable;

      if (String.IsNullOrEmpty(path))
        throw new SoxException("SoX path not specified");

      if (File.Exists(path))
        soxExecutable = path;
      else
        throw new FileNotFoundException("SoX executable not found");

      using (SoxProcess versionCheck = new SoxProcess())
      {
        versionCheck.StartInfo.RedirectStandardOutput = true;
        versionCheck.StartInfo.FileName = soxExecutable;
        versionCheck.StartInfo.Arguments = "--version";
        versionCheck.Start();

        string output = versionCheck.StandardOutput.ReadLine();

        if (versionCheck.WaitForExit(1000) == false)
          throw new TimeoutException("Cannot obtain SoX version: response timeout");

        Match versionMatch = new Regex(@"\sSoX v(\d{1,2})\.(\d{1,2})\.(\d{1,2})").Match(output);

        if (!versionMatch.Success)
          throw new SoxException("Cannot obtain SoX version: unable to fetch info from Sox");

        try
        {
          int majorVersion = Int32.Parse(versionMatch.Groups[1].Value);
          int minorVersion = Int32.Parse(versionMatch.Groups[2].Value);
          int fixVersion = Int32.Parse(versionMatch.Groups[3].Value);

          if ((majorVersion < 14) ||
              ((majorVersion == 14) && (minorVersion < 3)) ||
              ((majorVersion == 14) && (minorVersion == 3) && (fixVersion < 1)))
            throw new SoxException(versionMatch.Groups[0] + " not currently supported");
        }

        catch (Exception ex)
        {
          throw new SoxException("Cannot obtain SoX version", ex);
        }
      }

      SoxProcess soxProc = new SoxProcess();
      soxProc.StartInfo.FileName = soxExecutable;
      soxProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(soxExecutable);

      return soxProc;
    }
  }
}
