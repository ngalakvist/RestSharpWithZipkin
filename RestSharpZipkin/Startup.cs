using Microsoft.Owin;
using Owin;
using RestSharp;
using System;
using System.Threading.Tasks;
using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;


namespace RestSharpZipkin
{
  class Startup
  {

    public void Configuration(IAppBuilder appBuilder)
    {
      TraceManager.SamplingRate = 1.0f;
      var logger = new ConsoleLogger();
      var httpSender = new HttpZipkinSender("http://localhost:9411", "application/json");
      var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
      TraceManager.RegisterTracer(tracer);
      TraceManager.Start(logger);
      // Setup Owin Middleware
      appBuilder.UseZipkinTracer("RestSharpZipkin");
      appBuilder.Run(RestSharpClientTrace);

      //
    }
    private static async Task RestSharpClientTrace(IOwinContext context)
    {

      IRestClient restSharpClient = new RestClient("http://localhost:9000/api");
      var restClient = new TraceRestClient(restSharpClient);

      var request = new RestRequest(Method.GET);
      GetTraceContext(request, context, restClient);
      var response = restClient.Execute(request);
      await context.Response.WriteAsync(response.ToString());
    }

    private static void GetTraceContext(IRestRequest request, IOwinContext context, TraceRestClient restSharpClient)
    {
      var b3FormatTrace = restSharpClient.RestClientHandler.GetTrace();
      var b3FormatTraceParsed = b3FormatTrace.Split('-');
      AddTraceHeaders(request, b3FormatTraceParsed, b3FormatTrace);
    }


    private static void AddTraceHeaders(IRestRequest request, string[] b3FormatParsed, string b3Format)
    {
      request.AddHeader("b3", b3Format);
      request.AddHeader("X-B3-TraceId", b3FormatParsed[0]);
      request.AddHeader("X-B3-SpanId", b3FormatParsed[1]);
      request.AddHeader("X-B3-ParentSpanId", b3FormatParsed[3]);
      System.Diagnostics.Trace.WriteLine("b3-Format: " + b3Format);
      System.Diagnostics.Trace.WriteLine("X-B3-TraceId: " + b3FormatParsed[0]);
      System.Diagnostics.Trace.WriteLine("X-B3-SpanId: " + b3FormatParsed[1]);
      System.Diagnostics.Trace.WriteLine("X-B3-ParentSpanId:" + b3FormatParsed[3]);

    }
  }

  internal class ConsoleLogger : ILogger
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
