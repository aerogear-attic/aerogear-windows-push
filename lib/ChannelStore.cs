using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    using Windows.Storage;

    public class ChannelStore : AeroGear.Push.IChannelStore
    {
        private const string STORE_KEY = "Channel";

        public void Save(string channel)
        {
            OpenSettings().Values[STORE_KEY] = channel;
        }

        public string Read()
        {
            return (string) OpenSettings().Values[STORE_KEY];
        }

        private ApplicationDataContainer OpenSettings()
        {
            return ApplicationData.Current.LocalSettings;
        }
    }
}
