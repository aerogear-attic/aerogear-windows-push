using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    public interface IChannelStore
    {
        string Read();
        void Save(string channel);
    }
}
