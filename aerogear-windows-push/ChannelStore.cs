using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace AeroGear.Push
{
    public class ChannelStore
    {
        public void Save(string channel)
        {
            Task<StorageFile> openFile = OpenFile(CreationCollisionOption.ReplaceExisting);
            openFile.Wait();
            Task writeContent = WriteContent(openFile.Result, channel);
            writeContent.Wait();
        }

        public string Read()
        {
            Task<StorageFile> openFile = OpenFile(CreationCollisionOption.OpenIfExists);
            openFile.Wait();
            Task<string> readContent = ReadContent(openFile.Result);
            readContent.Wait();
            return readContent.Result;
        }

        private async Task<StorageFile> OpenFile(CreationCollisionOption option)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            return await localFolder.CreateFileAsync("channelStore", option);
        }

        private async Task WriteContent(StorageFile file, string contents)
        {
            using (IRandomAccessStream textStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(contents);
                    await textWriter.StoreAsync();
                }
            }
        }

        private async Task<string> ReadContent(StorageFile file)
        {
            string contents;
            using (IRandomAccessStream textStream = await file.OpenReadAsync())
            {
                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    contents = textReader.ReadString(textLength);
                }
            }
            return contents;
        }
    }
}
