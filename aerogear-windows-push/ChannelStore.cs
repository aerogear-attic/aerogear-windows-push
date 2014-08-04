using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    using Windows.Storage;
    public class ChannelStore
    {
        public async void Save(string channel)
        {
            StorageFile file = await OpenFile(CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, channel);
        }

        public async Task<string> Read()
        {
            StorageFile file = await OpenFile(CreationCollisionOption.OpenIfExists);
            return await FileIO.ReadTextAsync(file);
        }

        private async Task<StorageFile> OpenFile(CreationCollisionOption option)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            return await localFolder.CreateFileAsync("channelStore", option);
        }
    }
}
