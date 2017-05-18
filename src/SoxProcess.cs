using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SoxSharp
{
  sealed class SoxProcess : Process
  {
    public RegexSet Regex { get; private set; }

    private static readonly byte[] InternalSoxHash = { 0x05, 0xd5, 0x00, 0x8a, 0x50, 0x56, 0xe2, 0x8e, 0x45, 0xad, 0x1e, 0xb7, 0xd7, 0xbe, 0x9f, 0x03 };


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

      if ((Environment.OSVersion.Platform == PlatformID.MacOSX) ||
          (Environment.OSVersion.Platform == PlatformID.Unix))
      {
        if (String.IsNullOrEmpty(path))
          throw new SoxException("SoX path not specified");

        if (File.Exists(path))
          soxExecutable = path;
        else
          throw new FileNotFoundException("SoX executable not found");
      }

      else
      {
        if (String.IsNullOrEmpty(path))
        {
          // The SoX executable is directly extracted from the library resources
          // and only if it was not previously extracted (file MD5 hash check is
          // performed to ensure it is the expected one).

          try
          {
            soxExecutable = Path.Combine(Path.GetTempPath(), "sox.exe");

            if (!File.Exists(soxExecutable))
              File.WriteAllBytes(soxExecutable, SoxSharp.Resources.sox);
            else
            {
              using (MD5 md5 = MD5.Create())
              {
                using (FileStream stream = File.OpenRead(soxExecutable))
                {
                  byte[] hash = md5.ComputeHash(stream);

                  if (!InternalSoxHash.SequenceEqual(hash))
                    File.WriteAllBytes(soxExecutable, SoxSharp.Resources.sox);
                }
              }
            }
          }

          catch (Exception ex)
          {
            throw new SoxException("Cannot extract SoX executable", ex);
          }
        }
        else
        {
          if (File.Exists(path))
            soxExecutable = path;
          else
            throw new FileNotFoundException("SoX executable not found");
        }
      }

      RegexSet soxRegex = null;

      using (SoxProcess versionCheck = new SoxProcess())
      {
        versionCheck.StartInfo.RedirectStandardOutput = true;
        versionCheck.StartInfo.FileName = soxExecutable;
        versionCheck.StartInfo.Arguments = "-h";
        versionCheck.Start();

        string output = versionCheck.StandardOutput.ReadLine();

        if (versionCheck.WaitForExit(1000) == false)
          throw new TimeoutException("Cannot obtain SoX version: response timeout");

        Match versionMatch = new Regex(@"\sSoX v(\d{1,2}\.\d{1,2}\.\d{1,2})").Match(output);

        if (!versionMatch.Success)
          throw new SoxException("Cannot obtain SoX version: unable to fetch version info from SoX help");

        string soxVersion = versionMatch.Groups[1].Value;
        soxRegex = RegexSet.GetRegexSet(soxVersion);

        if (soxRegex == null)
          throw new SoxException("SoX version " + soxVersion + " not currently supported");
      }

      SoxProcess soxProc = new SoxProcess();
      soxProc.StartInfo.FileName = soxExecutable;
      soxProc.Regex = soxRegex;

      return soxProc;
    }
  }
}
