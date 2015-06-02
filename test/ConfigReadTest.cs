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
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    [TestClass]
    public class ConfigReadTest
    {
        [TestMethod]
        public async Task shouldReadLocalPushConfig()
        {
            //given
            var registration = new WnsRegistration();

            //when
            var config = await registration.LoadConfigJson("push-config.json");

            //then
            Assert.AreNotEqual(null, config);
            Assert.AreEqual(new Uri("http://localhost:8080/ag-push"), config.UnifiedPushUri);
            Assert.IsTrue(config.Categories.Contains("one"));
        }
    }
}
