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

namespace RestSharpZipkin
{
  public class TraceRestClient : IRestClient
  {
    private readonly IRestClient _innerClient;
    public TracingRestClientHandler RestClientHandler;

    public TraceRestClient(IRestClient innerClient)

    {
      RestClientHandler = TracingRestClientHandler.WithoutInnerHandler("RestSharpZipkin");
      _innerClient = innerClient;

    }

    public IAuthenticator Authenticator
    {
      get
      {
        return _innerClient.Authenticator;
      }

      set
      {
        _innerClient.Authenticator = value;
      }
    }

    public Uri BaseUrl
    {
      get
      {
        return _innerClient.BaseUrl;
      }

      set
      {
        _innerClient.BaseUrl = value;
      }
    }

    public RequestCachePolicy CachePolicy
    {
      get
      {
        return _innerClient.CachePolicy;
      }

      set
      {
        _innerClient.CachePolicy = value;
      }
    }

    public X509CertificateCollection ClientCertificates
    {
      get
      {
        return _innerClient.ClientCertificates;
      }

      set
      {
        _innerClient.ClientCertificates = value;
      }
    }

    public CookieContainer CookieContainer
    {
      get
      {
        return _innerClient.CookieContainer;
      }

      set
      {
        _innerClient.CookieContainer = value;
      }
    }

    public IList<Parameter> DefaultParameters
    {
      get
      {
        return _innerClient.DefaultParameters;
      }
    }

    public Encoding Encoding
    {
      get
      {
        return _innerClient.Encoding;
      }

      set
      {
        _innerClient.Encoding = value;
      }
    }

    public bool FailOnDeserializationError { get; set; }

    public bool FollowRedirects
    {
      get
      {
        return _innerClient.FollowRedirects;
      }

      set
      {
        _innerClient.FollowRedirects = value;
      }
    }

    public int? MaxRedirects
    {
      get
      {
        return _innerClient.MaxRedirects;
      }

      set
      {
        _innerClient.MaxRedirects = value;
      }
    }

    public bool PreAuthenticate
    {
      get
      {
        return _innerClient.PreAuthenticate;
      }

      set
      {
        _innerClient.PreAuthenticate = value;
      }
    }

    public IWebProxy Proxy
    {
      get
      {
        return _innerClient.Proxy;
      }

      set
      {
        _innerClient.Proxy = value;
      }
    }

    public int ReadWriteTimeout
    {
      get
      {
        return _innerClient.ReadWriteTimeout;
      }

      set
      {
        _innerClient.ReadWriteTimeout = value;
      }
    }

    public int Timeout
    {
      get
      {
        return _innerClient.Timeout;
      }

      set
      {
        _innerClient.Timeout = value;
      }
    }

    public string UserAgent
    {
      get
      {
        return _innerClient.UserAgent;
      }

      set
      {
        _innerClient.UserAgent = value;
      }
    }

    public bool UseSynchronizationContext
    {
      get
      {
        return _innerClient.UseSynchronizationContext;
      }

      set
      {
        _innerClient.UseSynchronizationContext = value;
      }
    }

    public bool AutomaticDecompression
    {
      get
      {
        return _innerClient.AutomaticDecompression;
      }

      set
      {
        _innerClient.AutomaticDecompression = value;
      }
    }

    public string ConnectionGroupName
    {
      get
      {
        return _innerClient.ConnectionGroupName;
      }

      set
      {
        _innerClient.ConnectionGroupName = value;
      }
    }

    public bool UnsafeAuthenticatedConnectionSharing
    {
      get
      {
        return _innerClient.UnsafeAuthenticatedConnectionSharing;
      }

      set
      {
        _innerClient.UnsafeAuthenticatedConnectionSharing = value;
      }
    }

    public string BaseHost
    {
      get
      {
        return _innerClient.BaseHost;
      }

      set
      {
        _innerClient.BaseHost = value;
      }
    }

    public bool AllowMultipleDefaultParametersWithSameName
    {
      get
      {
        return _innerClient.AllowMultipleDefaultParametersWithSameName;
      }

      set
      {
        _innerClient.AllowMultipleDefaultParametersWithSameName = value;
      }
    }

    public bool Pipelined
    {
      get
      {
        return _innerClient.Pipelined;
      }

      set
      {
        _innerClient.Pipelined = value;
      }
    }

    public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
    {
      get
      {
        return _innerClient.RemoteCertificateValidationCallback;
      }

      set
      {
        _innerClient.RemoteCertificateValidationCallback = value;
      }
    }

    public void AddHandler(string contentType, IDeserializer deserializer)
    {
      _innerClient.AddHandler(contentType, deserializer);
    }

    public void AddHandler(string contentType, Func<IDeserializer> deserializerFactory)
    {
      _innerClient.AddHandler(contentType, deserializerFactory);
    }

    public Uri BuildUri(IRestRequest request)
    {
      return _innerClient.BuildUri(request);
    }

    public string BuildUriWithoutQueryParameters(IRestRequest request)
    {
      return _innerClient.BuildUriWithoutQueryParameters(request);
    }

    public void ClearHandlers()
    {
      _innerClient.ClearHandlers();
    }

    public byte[] DownloadData(IRestRequest request)
    {
      return _innerClient.DownloadData(request);
    }

    public IRestClient UseQueryEncoder(Func<string, Encoding, string> queryEncoder)
    {
      return _innerClient.UseQueryEncoder(queryEncoder);
    }

    public virtual IRestResponse Execute(IRestRequest request)
    {

      return _innerClient.Execute(request);

    }
    private static string Getter(HttpHeader carrier, string key)
    {
      return key;
    }

    public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
    {
      return _innerClient.Execute<T>(request);
    }

    public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
    {
      return _innerClient.ExecuteAsGet(request, httpMethod);
    }

    public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
    {
      return _innerClient.ExecuteAsGet<T>(request, httpMethod);
    }

    public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
    {
      return _innerClient.ExecuteAsPost(request, httpMethod);
    }

    public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
    {
      return _innerClient.ExecuteAsPost<T>(request, httpMethod);
    }

    public IRestClient UseSerializer(IRestSerializer serializer)
    {
      return _innerClient.UseSerializer(serializer);
    }

    public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
    {
      return _innerClient.ExecuteAsync(request, callback);
    }

    public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
    {

      return _innerClient.ExecuteAsync<T>(request, callback);
    }

    public RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
    {
      return _innerClient.ExecuteAsyncGet(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
    {

      return _innerClient.ExecuteAsyncGet<T>(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
    {

      return _innerClient.ExecuteAsyncPost(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
    {
      return _innerClient.ExecuteAsyncPost<T>(request, callback, httpMethod);
    }

    public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
    {
      return _innerClient.ExecuteGetTaskAsync(request);
    }

    public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecuteGetTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
    {
      return _innerClient.ExecuteGetTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecuteGetTaskAsync<T>(request, token);
    }

    public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
    {
      return _innerClient.ExecutePostTaskAsync(request);
    }

    public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecutePostTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
    {
      return _innerClient.ExecutePostTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecutePostTaskAsync<T>(request, token);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
    {
      return _innerClient.ExecuteTaskAsync(request);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecuteTaskAsync(request, token);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
    {
      return _innerClient.ExecuteTaskAsync<T>(request);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
    {
      return _innerClient.ExecuteTaskAsync<T>(request, token);
    }

    public void RemoveHandler(string contentType)
    {
      _innerClient.RemoveHandler(contentType);
    }


    public IRestResponse<T> Deserialize<T>(IRestResponse response)
    {
      return _innerClient.Deserialize<T>(response);
    }

    public IRestClient UseUrlEncoder(Func<string, string> encoder)
    {
      throw new NotImplementedException();
    }

    public IRestResponse Execute(IRestRequest request, Method httpMethod)
    {
      return _innerClient.Execute(request, httpMethod);

    }

    public IRestResponse<T> Execute<T>(IRestRequest request, Method httpMethod) where T : new()
    {
      return _innerClient.Execute<T>(request, httpMethod);
    }

    public byte[] DownloadData(IRestRequest request, bool throwOnError)
    {
      return _innerClient.DownloadData(request, throwOnError);
    }

    public void ConfigureWebRequest(Action<HttpWebRequest> configurator)
    {
      _innerClient.ConfigureWebRequest(configurator);
    }

    public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, Method httpMethod)
    {
      return _innerClient.ExecuteTaskAsync<T>(request, httpMethod);
    }

    public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token, Method httpMethod)
    {
      return _innerClient.ExecuteTaskAsync(request, token, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, Method httpMethod)
    {
      return _innerClient.ExecuteAsync(request, callback, httpMethod);
    }

    public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, Method httpMethod)
    {
      return _innerClient.ExecuteAsync<T>(request, callback, httpMethod);
    }
  }
}
