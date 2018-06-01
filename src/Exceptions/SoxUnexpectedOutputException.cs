using System;


namespace SoxSharp.Exceptions
{
  /// <summary>
  /// Exception that is thrown when SoX output doesn't match the expected regular expression.
  /// </summary>
  public class SoxUnexpectedOutputException: SoxException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxUnexpectedOutputException"/> class with an output string.
    /// </summary>
    /// <param name="output">SoX unexpected output string.</param>
    public SoxUnexpectedOutputException(string output)
    : base(output)
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxUnexpectedOutputException"/> class with an output string
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="output">SoX unexpected output string.</param>
    /// <param name="inner">Exception that caused it.</param>
    public SoxUnexpectedOutputException(string output, Exception inner)
    : base(output, inner)
    {
    }
  }
}
