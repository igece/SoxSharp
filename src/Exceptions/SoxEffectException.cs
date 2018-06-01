using System;


namespace SoxSharp.Exceptions
{
  /// <summary>
  /// Exception that is thrown when trying to apply an Effect which caontains invalid data.
  /// </summary>
  public class SoxEffectException : SoxException
  {
    /// <summary>
    /// Name of the effect that caused the exception.
    /// </summary>
    public string EffectName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxEffectException"/> class with its message string set
    /// to a default message.
    /// </summary>
    /// <param name="effectName">The effect name.</param>
    public SoxEffectException(string effectName)
    {
      EffectName = effectName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxEffectException"/> class with a specified message.
    /// </summary>
    /// <param name="effectName">The effect name.</param>
    /// <param name="message">The exception's message.</param>
    public SoxEffectException(string effectName, string message)
    : base(message)
    {
      EffectName = effectName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SoxSharp.SoxEffectException"/> class with a specified message and a
    /// reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="effectName">The effect name.</param>
    /// <param name="message">The exception's message.</param>
    /// <param name="inner">Exception that caused it.</param>
    public SoxEffectException(string effectName, string message, Exception inner)
    : base(message, inner)
    {
      EffectName = effectName;
    }
  }
}
