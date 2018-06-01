using System;
using System.Globalization;
using System.Linq;


namespace SoxSharp
{
  static class Utils
  {
    private static int[] weights2 = { 60, 1 };
    private static int[] weights3 = { 60 * 60, 60, 1 };


    public static TimeSpan TimeSpanFromString(string time)
    {
      string[] parts = time.Split(':');

      switch (parts.Length)
      {
        case 1:
          return TimeSpan.FromSeconds(Convert.ToDouble(parts[0], CultureInfo.InvariantCulture));

        case 2:
          return TimeSpan.FromSeconds(parts.Zip(weights2, (d, w) => String.IsNullOrEmpty(d) ? 0 : Convert.ToDouble(d, CultureInfo.InvariantCulture) * w).Sum());

        case 3:
          return TimeSpan.FromSeconds(parts.Zip(weights3, (d, w) => String.IsNullOrEmpty(d) ? 0 : Convert.ToDouble(d, CultureInfo.InvariantCulture) * w).Sum());

        default:

          throw new ArgumentException("Invalid time string");
      }
    }
  }
}
