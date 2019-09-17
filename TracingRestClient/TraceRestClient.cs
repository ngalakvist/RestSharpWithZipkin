using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zipkin4net.Middleware;

namespace RestSharpZipkin
{ /// <summary>
/// RestSharp wrapper that user zipkin to generate traces
/// </summary>
  public class TraceRestClient : IRestClient
  {
    private IRestClient innerClient;
    public TracingRestClientHandler restClientHandler;
    private ZipkinB3TraceHeaderInjector traceHeaderInjector;
    /// <summary>
    /// RestSharpZipkinTraceClient .
    /// Injects  b3 headers to all request.
    /// </summary>
    /// <param name="innerClient"></param>
    /// <param name="restClientAppName"></param>
    /// <param name="zipkinServerName"></param>
    public TraceRestClient(IRestClient innerClient, string restClientAppName, string zipkinServerName)
    {
      SetUpRestSharpZipkinTracerClient(innerClient, restClientAppName, zipkinServerName);
    }

    private void SetUpRestSharpZipkinTracerClient(IRestClient innerClient, string restClientAppName,
      string zipkinServerName)
    {
      restClientHandler = TracingRestClientHandler.WithoutInnerHandler(restClientAppName);
      this.innerClient = innerClient ?? throw new ArgumentNullException("innerClient");
      restClientAppName = restClientAppName ?? throw new ArgumentNullException("restClientAppName");
      zipkinServerName = zipkinServerName ?? throw new ArgumentNullException("zipkinServerName");
      traceHeaderInjector = new ZipkinB3TraceHeaderInjector(restClientAppName, zipkinServerName);
    }

    public TracingRestClientHandler RestClientHandler => restClientHandler;

    public IAuthenticator Authenticator
    {
      get
      {
        return innerClient.Authenticator;
      }

      set
      {
        innerClient.Authenticator = value;
      }
    }

    public Uri BaseUrl
    {
      get
      {
        return innerClient.BaseUrl;
      }

      set
      {
        innerClient.BaseUrl = value;
      }
    }

    public RequestCachePolicy CachePolicy
    {
      get
      {
        return innerClient.CachePolicy;
      }

      set
      {
        innerClient.CachePolicy = value;
      }
    }

    public X509CertificateCollection ClientCertificates
    {
      get
      {
        return innerClient.ClientCertificates;
      }

      set
      {
        innerClient.ClientCertificates = value;
      }
    }

    public CookieContainer CookieContainer
    {
      get
      {
        return innerClient.CookieContainer;
      }

      set
      {
        innerClient.CookieContainer = value;
      }
    }

    public IList<Parameter> DefaultParameters
    {
      get
      {
        return innerClient.DefaultParameters;
      }
    }

    public Encoding Encoding
    {
      get
      {
        return innerClient.Encoding;
      }

      set
      {
        innerClient.Encoding = value;
      }
    }

    public bool FailOnDeserializationError { get; set; }

    public bool FollowRedirects
    {
      get
      {
        return innerClient.FollowRedirects;
      }

      set
      {
        innerClient.FollowRedirects = value;
      }
    }

    public int? MaxRedirects
    {
      get
      {
        return innerClient.MaxRedirects;
      }

      set
      {
        innerClient.MaxRedirects = value;
      }
    }

    public bool PreAuthenticate
    {
      get
      {
        return innerClient.PreAuthenticate;
      }

      set
      {
        innerClient.PreAuthenticate = value;
      }
    }

    public IWebProxy Proxy
    {
      get
      {
        return innerClient.Proxy;
      }

      set
      {
        innerClient.Proxy = value;
      }
    }

    public int ReadWriteTimeout
    {
      get
      {
        return innerClient.ReadWriteTimeout;
      }

      set
      {
        innerClient.ReadWriteTimeout = value;
      }
    }

    public int Timeout
    {
      get
      {
        return innerClient.Timeout;
      }

      set
      {
        innerClient.Timeout = value;
      }
    }

    public string UserAgent
    {
      get
      {
        return innerClient.UserAgent;
      }

      set
      {
        innerClient.UserAgent = value;
      }
    }

    public bool UseSynchronizationContext
    {
      get
      {
        return innerClient.UseSynchronizationContext;
      }

      set
      {
        innerClient.UseSynchronizationContext = value;
      }
    }

    public bool AutomaticDecompression
    {
      get
      {
        return innerClient.AutomaticDecompression;
      }

      set
      {
        innerClient.AutomaticDecompression = value;
      }
    }

    public string ConnectionGroupName
    {
      get
      {
        return innerClient.ConnectionGroupName;
      }

      set
      {
        innerClient.ConnectionGroupName = value;
      }
    }

    public bool UnsafeAuthenticatedConnectionSharing
    {
      get
      {
        return innerClient.UnsafeAuthenticatedConnectionSharing;
      }

      set
      {
        innerClient.UnsafeAuthenticatedConnectionSharing = value;
      }
    }

    public string BaseHost
    {
      get
      {
        return innerClient.BaseHost;
      }

      set
      {
        innerClient.BaseHost = value;
      }
    }

    public bool AllowMultipleDefaultParametersWithSameName
    {
      get
      {
        return innerClient.AllowMultipleDefaultParametersWithSameName;
      }

      set
      {
        innerClient.AllowMultipleDefaultParametersWithSameName = value;
      }
    }

    public bool Pipelined
    {
      get
      {
        return innerClient.Pipelined;
      }

      set
      {
        innerClient.Pipelined = value;
      }
    }

    public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
    {
      get
      {
        return innerClient.RemoteCertificateValidationCallback;
      }

      set
      {
        innerClient.RemoteCertificateValidationCallback = value;
      }
    }

    public void AddHandler(string contentType, IDeserializer deserializer)
    {
      innerClient.AddHandler(contentType, deserializer);
    }

    public void AddHandler(string contentType, Func<IDeserializer> deserializerFactory)
    {
      innerClient.AddHandler(contentType, deserializerFactory);
    }

    public Uri BuildUri(IRestRequest request)
    {
      return innerClient.BuildUri(request);
    }

    public string BuildUriWithoutQueryParameters(IRestRequest request)
    {
      return innerClient.BuildUriWithoutQueryParameters(request);
    }

    public void ClearHandlers()
    {
      innerClient.ClearHandlers();
    }

    public byte[] DownloadData(IRestRequest request)
    {
      return innerClient.DownloadData(request);
    }

    public IRestClient UseQueryEncoder(Func<string, Encoding, string> queryEncoder)
    {
      return innerClient.UseQueryEncoder(queryEncoder);
    }

    public virtual IRestResponse Execute(IRestRequest request)
    {

      traceHeaderInjector.InjectB3TraceToRequestHeaders(request, this);
      return innerClient.Execute(request);

    }
    private static string Getter(HttpHeader carrier, string key)
    {
      return key;
    }

    public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
    {
      return innerClient.Execute<T>(request);
    }

    public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
    {
      return innerClient.ExecuteAsGet(request, httpMethod);
    }

    public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
    {
      return innerClient.ExecuteAsGet<T>(request, httpMethod);
    }

    public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
    {
      return innerClient.ExecuteAsPost(request, httpMethod);
    }

    public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
    {
      return innerClient.ExecuteAsPost<T>(request, httpMethod);
    }

    public IRestClient UseSerializer(IRestSerializer serializer)
    {
      return innerClient.UseSerializer(serializer);
    }

    public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
    {
      return innerClient.ExecuteAsync(request, callback);
    }

    public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
    {

      return innerClient.ExecuteAsync<T>(request, callback);
    }

    public RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
    {
      return innerClient.ExecuteAsyncGet(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
    {

      return innerClient.ExecuteAsyncGet<T>(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
    {

      return innerClient.ExecuteAsyncPost(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
    {
      return innerClient.ExecuteAsyncPost<T>(request, callback, httpMethod);
    }

    public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
    {
      return innerClient.ExecuteGetTaskAsync(request);
    }

    public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecuteGetTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
    {
      return innerClient.ExecuteGetTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecuteGetTaskAsync<T>(request, token);
    }

    public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
    {
      return innerClient.ExecutePostTaskAsync(request);
    }

    public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecutePostTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
    {
      return innerClient.ExecutePostTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecutePostTaskAsync<T>(request, token);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
    {
      return innerClient.ExecuteTaskAsync(request);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecuteTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
    {
      return innerClient.ExecuteTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return innerClient.ExecuteTaskAsync<T>(request, token);
    }

    public void RemoveHandler(string contentType)
    {
      innerClient.RemoveHandler(contentType);
    }


    public IRestResponse<T> Deserialize<T>(IRestResponse response)
    {
      return innerClient.Deserialize<T>(response);
    }

    public IRestClient UseUrlEncoder(Func<string, string> encoder)
    {
      throw new NotImplementedException();
    }

    public IRestResponse Execute(IRestRequest request, Method httpMethod)
    {
      return innerClient.Execute(request, httpMethod);

    }

    public IRestResponse<T> Execute<T>(IRestRequest request, Method httpMethod) where T : new()
    {
      return innerClient.Execute<T>(request, httpMethod);
    }

    public byte[] DownloadData(IRestRequest request, bool throwOnError)
    {
      return innerClient.DownloadData(request, throwOnError);
    }

    public void ConfigureWebRequest(Action<HttpWebRequest> configurator)
    {
      innerClient.ConfigureWebRequest(configurator);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, Method httpMethod)
    {
      return innerClient.ExecuteTaskAsync<T>(request, httpMethod);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token, Method httpMethod)
    {
      return innerClient.ExecuteTaskAsync(request, token, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, Method httpMethod)
    {
      return innerClient.ExecuteAsync(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, Method httpMethod)
    {
      return innerClient.ExecuteAsync<T>(request, callback, httpMethod);
    }
  }
}
