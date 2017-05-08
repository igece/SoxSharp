using System;


namespace SoxSharp
{
  /// <summary>
  /// Exception that is thrown when a SoX operation fails.
  public class SoxException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxException"/> clas with its message string set
    /// to a default message.
    /// </summary>
  	public SoxException()
  	{
  	}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxException"/> class with a specified message.
    /// </summary>
    /// <param name="message">The exception's message.</param>
  	public SoxException(string message)
  		: base(message)
  	{
  	}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxException"/> class with a specified message and a
    /// reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The exception's message.</param>
    /// <param name="inner">Exception that caused it.</param>
  	public SoxException(string message, Exception inner)
  		: base(message, inner)
  	{
  	}
  }
}
