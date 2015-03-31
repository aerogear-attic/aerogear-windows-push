using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroGear.Push
{
    public abstract class Registration
    {
        public event EventHandler<PushReceivedEvent> PushReceivedEvent;

        public async Task<string> Register(PushConfig pushConfig)
        {
            return await Register(pushConfig, CreateUPSHttpClient(pushConfig));
        }

        public async Task<string> Register(PushConfig pushConfig, IUPSHttpClient client)
        {
            Installation installation = CreateInstallation(pushConfig);
            IChannelStore channelStore = CreateChannelStore();
            string channelUri = await ChannelUri();
            var token = pushConfig.VariantId + channelUri;
            if (!token.Equals(channelStore.Read()))
            {
                installation.deviceToken = channelUri;
                await client.register(installation);
                channelStore.Save(token);
            }
            return installation.deviceToken;
        }

        protected void OnPushNotification(string message, IDictionary<string, string> data)
        {
            EventHandler<PushReceivedEvent> handler = PushReceivedEvent;
            if (handler != null)
            {
                handler(this, new PushReceivedEvent(new PushNotification() {message = message, data = data}));
            }
        }

        private IUPSHttpClient CreateUPSHttpClient(PushConfig pushConfig)
        {
            return new UPSHttpClient(pushConfig.UnifiedPushUri, pushConfig.VariantId, pushConfig.VariantSecret);
        }

        protected abstract Installation CreateInstallation(PushConfig pushConfig);

        protected abstract IChannelStore CreateChannelStore();

        protected abstract Task<string> ChannelUri();
    }
}
