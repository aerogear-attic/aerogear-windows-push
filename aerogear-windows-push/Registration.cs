using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    public abstract class Registration
    {
        public event EventHandler<PushReceivedEvent> PushReceivedEvent;

        public void Register(PushConfig pushConfig)
        {
            Installation installation = CreateInstallation(pushConfig);
            RegisterAsync(installation, CreateUPSHttpClient(pushConfig)).Wait();
        }

        public void Register(PushConfig pushConfig, IUPSHttpClient client)
        {
            RegisterAsync(CreateInstallation(pushConfig), client).Wait();
        }

        protected void OnPushNotification(string message, Dictionary<string, string> data)
        {
            EventHandler<PushReceivedEvent> handler = PushReceivedEvent;
            if (handler != null)
            {
                handler(this, new PushReceivedEvent(new PushNotification() {message = message, data = data}));
            }
        }

        protected abstract Task RegisterAsync(Installation installation, IUPSHttpClient iUPSHttpClient);

        protected IUPSHttpClient CreateUPSHttpClient(PushConfig pushConfig)
        {
            return new UPSHttpClient(pushConfig.UnifiedPushUri, pushConfig.VariantId, pushConfig.VariantSecret);
        }

        protected abstract Installation CreateInstallation(PushConfig pushConfig);
    }
}
