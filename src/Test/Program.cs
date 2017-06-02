using System;
using SoxSharp;


namespace Test
{
  class Test
  {
    public static void Main(string[] args)
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

        Console.WriteLine("SoXSharp Test App\n");

        Console.WriteLine("File Information");

        FileInfo wavInfo = sox.GetInfo("test.wav");
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
  }
}
