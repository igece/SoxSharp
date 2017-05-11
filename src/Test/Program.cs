using System;
using SoxSharp;


namespace Test
{
  class Test
  {
    public static void Main(string[] args)
    {
      using (Sox sox = new Sox("/Users/igece/Downloads/sox-14.4.2/sox"))
      {
        Console.WriteLine("SoXSharp Test App\n");

        Console.WriteLine("File Information");

        FileInfo wavInfo = sox.GetInfo("test.wav");
        Console.WriteLine(wavInfo);

        Console.WriteLine("Simple Conversion");

        sox.Output.Type = FileType.MP3;
        sox.Output.Comment = "Converted using SoX & SoXSharp";

        sox.Process("test.wav", "test.cvsd");
      }
    }
  }
}
