using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Networking.PushNotifications;

namespace AeroGear.Push
{
    public class PushReceivedEvent : EventArgs
    {
        public PushReceivedEvent(PushNotificationReceivedEventArgs Args)
        {
            this.Args = Args;
        }

        public PushNotificationReceivedEventArgs Args { get; set; }
    }

    public class RegistrationCompleteEvent
    {
        public RegistrationCompleteEvent(string Uri)
        {
            this.Uri = Uri;
        }

        public string Uri { get; set; }
    }
}
