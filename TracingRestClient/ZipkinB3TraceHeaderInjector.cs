using RestSharp;
using RestSharpZipkin;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace zipkin4net.Middleware
{
  public class ZipkinB3TraceHeaderInjector
  {
    private readonly string serviceName;
    private readonly string zipkinServerName;
    private Trace trace;

    /// <summary>
    /// Inject b3 tracer to headers
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="zipKinServerName"></param>
    public ZipkinB3TraceHeaderInjector(string serviceName, string zipKinServerName)
    {
      this.serviceName = serviceName;
      this.zipkinServerName = zipKinServerName;
    }

    /// <summary>
    /// Inject to Rest Client
    /// </summary>
    /// <param name="request"></param>
    /// <param name="restSharpClient"></param>
    public void InjectB3TraceToRequestHeaders(IRestRequest request, TraceRestClient restSharpClient)
    {
      InitialiseZipkinSetup();
      SetUpTracing();
      GetTraceContextAndAddHeaders(request, restSharpClient);
    }
    private void SetUpTracing()
    {
      trace = Trace.Create();
      Trace.Current = trace;
      using (var serverTrace = new ServerTrace(serviceName, "TraceRestClient"))
      {
        trace.Record(Annotations.Tag("http.host", "http.url"));
        trace.Record(Annotations.Tag("http.url", "http.url"));
        trace.Record(Annotations.Tag("http.path", "http.url"));
      }
    }

    private void InitialiseZipkinSetup()
    {
      TraceManager.SamplingRate = 1.0f;
      var logger = new TracingRestClient.Logger();
      var httpSender = new HttpZipkinSender(zipkinServerName, "application/json");
      var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
      TraceManager.RegisterTracer(tracer);
      TraceManager.Start(logger);
    }


    private static void GetTraceContextAndAddHeaders(IRestRequest request, TraceRestClient restSharpClient)
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
}
