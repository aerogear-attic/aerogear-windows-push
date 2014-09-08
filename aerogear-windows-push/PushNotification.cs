using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroGear.Push
{
    public class PushNotification
    {
        public string message { get; set; }
        public Dictionary<string, string> data { get; set; }
    }
}
