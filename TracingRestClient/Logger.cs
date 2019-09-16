using System;
using zipkin4net;

namespace TracingRestClient
{
  public class Logger : ILogger
  {

    public void LogInformation(string message)
    {
      Console.WriteLine(message);
    }

    public void LogWarning(string message)
    {
      Console.WriteLine(message);
    }

    public void LogError(string message)
    {
      Console.WriteLine(message);
    }
  }

}

