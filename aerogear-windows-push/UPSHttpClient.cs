using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AeroGear.Push
{
    public sealed class UPSHttpClient : IDisposable, IUPSHttpClient
    {
        private const string AUTHORIZATION_HEADER = "Authorization";
        private const string AUTHORIZATION_METHOD = "Basic";
        private const string REGISTRATION_ENDPOINT = "rest/registry/device";

        private HttpClient httpClient;

        public UPSHttpClient(Uri uri, string username, string password)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER, AUTHORIZATION_METHOD + " " + CreateHash(username, password));
        }

        public HttpResponseMessage register(Installation installation)
        {
            return httpClient.PostAsJsonAsync<Installation>(REGISTRATION_ENDPOINT, installation).Result;
        }

        private static string CreateHash(string username, string password)
        {
            return Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(username + ":" + password));
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}