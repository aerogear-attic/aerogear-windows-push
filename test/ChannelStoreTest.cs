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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    [TestClass]
    public class ChannelStoreTest
    {
        [TestMethod]
        public void ShouldRead()
        {
            //given
            LocalStore store = new LocalStore();

            //when
            string result = store.Read("key");
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ShouldStore()
        {
            //given
            string key = "thekey";
            LocalStore store = new LocalStore();
            string channel = "dummy channel string";

            //when
            store.Save(key, channel);

            //then
            string result = store.Read(key);
            Assert.AreEqual(channel, result);
        }
    }
}
