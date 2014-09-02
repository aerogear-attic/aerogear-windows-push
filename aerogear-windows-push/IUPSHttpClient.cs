using System;
using System.Net.Http;

namespace AeroGear.Push
{
    public interface IUPSHttpClient: IDisposable
    {
        HttpResponseMessage register(Installation installation);
    }
}
