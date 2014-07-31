using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroGear.Push;

namespace test
{
    [TestClass]
    public class ChannelStoreTest
    {
        [TestMethod]
        public void ShouldRead()
        {
            //given
            ChannelStore store = new ChannelStore();

            //when
            string result = store.Read();
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ShouldStore()
        {
            //given
            ChannelStore store = new ChannelStore();
            string channel = "dummy channel string";

            //when
            store.Save(channel);

            //then
            string result = store.Read();
            Assert.AreEqual(channel, result);
        }
    }
}
