using System;
using System.Linq;


namespace SoxSharp
{
  static class Utils
  {
    private static int[] weights = { 60 * 60 * 1000, 60 * 1000, 1000, 10 };


    public static TimeSpan TimeSpanFromString(string time)
    {
      return TimeSpan.FromMilliseconds(time.Split('.', ':').Zip(weights, (d, w) => Convert.ToInt64(d) * w).Sum());
    }
  }
}
