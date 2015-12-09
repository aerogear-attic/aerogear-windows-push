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
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace AeroGear.Push
{
    [TestClass]
    public class RegistrationTest
    {
        [TestMethod]
        public async Task ShouldRegister()
        {
            //given
            var httpClient = new MockUpsHttpClient();
            Registration registration = new WnsRegistration();

            //when
            await registration.Register(new PushConfig {VariantId = "dummy"}, httpClient);

            //then
            Assert.IsTrue(httpClient.Installation.deviceToken != null);
        }

        [TestMethod]
        public void ShouldCreateUrlWithoutEndingSlash()
        {
            //given
            var uri = new Uri("http://noslash/uri");

            //when
            var endpoint = new UPSHttpClient(uri, "dummy", "123").CreateEndpoint("test");

            //then
            Assert.AreEqual("http://noslash/uri/test", endpoint);
        }
    }

    public class MockUpsHttpClient : IUPSHttpClient
    {
        public Installation Installation { get; private set; }

        public Task<HttpStatusCode> Register(Installation installation)
        {
            Installation = installation;
            return Task.Run(() => HttpStatusCode.OK);
        }

        public Task<HttpStatusCode> SendMetrics(string pushMessageId)
        {
            return Task.Run(() => HttpStatusCode.OK);
        }
    }
}