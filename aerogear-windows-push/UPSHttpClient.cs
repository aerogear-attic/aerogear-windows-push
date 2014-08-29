using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    public sealed class UPSHttpClient : IDisposable
    {
        private const string AUTHORIZATION_HEADER = "Authorization";
        private const string AUTHORIZATION_METHOD = "Basic";
        private const string REGISTRATION_ENDPOINT = "rest/registry/device";

        private HttpClient httpClient;

        public UPSHttpClient(Uri uri)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<HttpResponseMessage> register(Installation installation)
        {
            return httpClient.PostAsJsonAsync<Installation>(REGISTRATION_ENDPOINT, installation);
        }

        public void setCredentials(string username, string password)
        {
            httpClient.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER, AUTHORIZATION_METHOD + " " + CreateHash(username, password));
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