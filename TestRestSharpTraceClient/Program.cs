using RestSharp;
using RestSharpZipkin;
using System;
using System.Diagnostics;

namespace TestRestSharpTraceClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Trace.WriteLine("Started :....." + new DateTime());
      IRestClient restSharpClient = new RestClient("http://localhost:9000/api");
      var restClient = new TraceRestClient(restSharpClient, "TestRestSharpTraceClient", "http://localhost:9411/");
      var request = new RestRequest(Method.GET);
      var response = restClient.Execute(request);
      Trace.WriteLine(response.Content);
      Trace.WriteLine("----------Done.--------");
      Console.ReadLine();

    }
  }
}
