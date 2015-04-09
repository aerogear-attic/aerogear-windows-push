/**
 * JBoss, Home of Professional Open Source
 * Copyright Red Hat, Inc., and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * 	http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    /// <summary>
    /// Implementation of the IUPSHttpClient.
    /// </summary>
    public sealed class UPSHttpClient : IUPSHttpClient
    {
        private const string AUTHORIZATION_HEADER = "Authorization";
        private const string AUTHORIZATION_METHOD = "Basic";
        private const string REGISTRATION_ENDPOINT = "rest/registry/device";

        private HttpWebRequest request;

        public UPSHttpClient(Uri uri, string username, string password)
        {
            request = (HttpWebRequest)WebRequest.Create(uri.ToString() + REGISTRATION_ENDPOINT);
            request.ContentType = "application/json";
            request.Headers[AUTHORIZATION_HEADER] = AUTHORIZATION_METHOD + " " + CreateHash(username, password);
            request.Method = "POST";
        }

        public async Task<HttpStatusCode> register(Installation installation)
        {
            using (var postStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Installation));
                serializer.WriteObject(postStream, installation);
            }

            HttpWebResponse responseObject = (HttpWebResponse) await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
            var responseStream = responseObject.GetResponseStream();
            var streamReader = new StreamReader(responseStream);

            await streamReader.ReadToEndAsync();
            return responseObject.StatusCode;
        }

        private static string CreateHash(string username, string password)
        {
            return Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(username + ":" + password));
        }
    }
}