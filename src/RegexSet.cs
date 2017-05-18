using System.Text.RegularExpressions;


namespace SoxSharp
{
  sealed class RegexSet
  {
    public readonly Regex FileInfo;
    public readonly Regex Progress;
    public readonly Regex Log;


    public RegexSet(Regex fileInfo, Regex progress, Regex log)
    {
      FileInfo = fileInfo;
      Progress = progress;
      Log = log;
    }


    public static RegexSet GetRegexSet(string version)
    {
      switch (version)
      {
        case "14.4.2":

          return new RegexSet(new Regex(@"Input File\s*: .+\r?\nChannels\s*: (\d+)\r?\nSample Rate\s*: (\d+)\r?\nPrecision\s*: ([\s\S]+?)\r?\nDuration\s*: (\d{2}:\d{2}:\d{2}\.?\d{2}?)[\s\S]+?\r?\nFile Size\s*: (\d+\.?\d{0,2}?[k|M|G]?)\r?\nBit Rate\s*: (\d+\.?\d{0,2}?[k|M|G]?)\r?\nSample Encoding\s*: (.+)"),
                              new Regex(@"In:(\d{1,3}\.?\d{0,2})%\s+(\d{2}:\d{2}:\d{2}\.?\d{0,2})\s+\[(\d{2}:\d{2}:\d{2}\.?\d{0,2})\]\s+Out:(\d+\.?\d{0,2}[k|M|G]?)"),
                              new Regex(@"(FAIL|WARN)\s(.+)"));

        default:

          return null;
      }
    }
  }
}
