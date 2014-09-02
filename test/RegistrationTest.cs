using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Net.Http;

namespace AeroGear.Push
{
    [TestClass]
    public class RegistrationTest
    {
        [TestMethod]
        public void ShouldRegister()
        {
            //given
            Registration registration = new Registration();

            //when
            var httpClient = new MockUPSHttpClient();
            registration.RegisterAsync(new Installation(), httpClient).Wait();

            //then
            Assert.IsTrue(httpClient.installation.deviceToken != null);
        }
    }

    public class MockUPSHttpClient: IUPSHttpClient 
    {
        public HttpResponseMessage register(Installation installation)
        {
            this.installation = installation;
            return new HttpResponseMessage();
        }

        public void Dispose() 
        { 
        }

        public Installation installation { get; set; }
    }
}
