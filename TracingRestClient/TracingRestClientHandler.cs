using System;
using System.Net.Http;
using System.Net.Http.Headers;
using zipkin4net;
using zipkin4net.Propagation;

namespace RestSharpZipkin
{
  public class TracingRestClientHandler
  {
    private readonly IInjector<HttpHeaders> _injector;
    private readonly string _serviceName;
    private readonly Func<HttpRequestMessage, string> _getClientTraceRpc;


    /// <param name="serviceName"></param>
    /// <param name="getClientTraceRpc"></param>
    /// <returns></returns>
    public static TracingRestClientHandler WithoutInnerHandler(string serviceName, Func<HttpRequestMessage, string> getClientTraceRpc = null)
      => new TracingRestClientHandler(Propagations.B3String.Injector<HttpHeaders>((carrier, key, value) => carrier.Add(key, value)), serviceName, getClientTraceRpc);

    /// <summary>
    /// Constructor used to create the handler without an inner handler.
    /// </summary>
    /// <param name="injector"></param>
    /// <param name="serviceName"></param>
    /// <param name="getClientTraceRpc"></param>
    private TracingRestClientHandler(IInjector<HttpHeaders> injector, string serviceName, Func<HttpRequestMessage, string> getClientTraceRpc = null)
    {
      _injector = injector;
      _serviceName = serviceName;
      _getClientTraceRpc = getClientTraceRpc ?? (request => request.Method.ToString());
    }
    /// <summary>
    /// B3SingleFormat Trace
    /// </summary>
    /// <returns></returns>
    public string GetTrace()
    {
      String parent = null;
      using (var clientTrace = new ClientTrace(_serviceName, null))
      {
        if (clientTrace.Trace != null)
        {
          var b3Format = B3SingleFormat.WriteB3SingleFormat(clientTrace.Trace.CurrentSpan);
          parent = b3Format;
        }

      }

      return parent;
    }
  }
}
