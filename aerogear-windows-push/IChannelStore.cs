using System;
namespace AeroGear.Push
{
    public interface IChannelStore
    {
        string Read();
        void Save(string channel);
    }
}
