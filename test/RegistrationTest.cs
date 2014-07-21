using AeroGear.Push;
using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace test
{
    [TestClass]
    public class RegistrationTest
    {
        [TestMethod]
        public void ShouldRegister()
        {
            //given
            PushConfig config = new PushConfig() { UnifiedPushUri = new Uri("http://localhost:8080/ag-push/"), VariantId = "6db5c09e-6787-492a-9798-1bc5a2915603", VariantSecret = "4fec0b5f-608e-4f07-b761-a245f58134a2" };
            Registration registration = new Registration();

            //when
            registration.Register(config);

            //then

        }
    }
}
