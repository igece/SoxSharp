using System;


namespace SoxSharp
{
  public class SoxException : Exception
  {
  	public SoxException()
  	{
  	}

  	public SoxException(string message)
  		: base(message)
  	{
  	}

  	public SoxException(string message, Exception inner)
  		: base(message, inner)
  	{
  	}
  }
}
