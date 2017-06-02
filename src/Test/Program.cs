using System;
using SoxSharp;
using SoxSharp.Exceptions;


namespace Test
{
  class Test
  {
    public static void Main(string[] args)
    {
      try
      {
        using (Sox sox = new Sox(args[0]))
        {
          sox.OnLogMessage += (sender, e) =>
          {
            Console.WriteLine(e.LogLevel + ": " + e.Message);
          };

          sox.OnProgress += (sender, e) =>
          {
            Console.Write("Processing... {0}%   {1} {2} {3}                \r", e.Progress, e.Processed, e.Remaining, e.OutputSize);
          };

          Console.WriteLine("SoXSharp Test Application\n");

          Console.WriteLine("File Information");

          AudioInfo wavInfo = sox.GetInfo("test.wav");
          Console.WriteLine(wavInfo);

          Console.WriteLine("Simple Conversion");

          sox.Output.Type = FileType.WAV;
          sox.Output.SampleRate = 32000;
          sox.Output.Comment = "Converted using SoX & SoXSharp";

          sox.Process("test.wav", "test.xxx");

          Console.WriteLine("\r\nConversion finished");
          Console.ReadKey();
        }
      }

      catch (SoxException ex)
      {
        Console.WriteLine("SOXSHARP EXCEPTION RAISED: " + ex.Message + "\n\n" + ex.StackTrace);
      }

      catch (Exception ex)
      {
        Console.WriteLine("EXCEPTION RAISED: " + ex.Message + "\n\n" + ex.StackTrace);
      }
    }
  }
}
