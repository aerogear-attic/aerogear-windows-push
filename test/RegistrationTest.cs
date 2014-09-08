using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

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
        public override Task<HttpStatusCode> register(Installation installation)
        {
            this.installation = installation;
            return new Task<HttpStatusCode>( () => new HttpStatusCode());
        }

        public Installation installation { get; set; }
    }
}
