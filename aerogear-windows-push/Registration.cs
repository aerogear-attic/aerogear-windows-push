using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Popups;

namespace AeroGear.Push
{
    public class Registration
    {
        public event EventHandler<PushReceivedEvent> PushReceivedEvent;

        public void Register(PushConfig pushConfig)
        {
            Installation installation = CreateInstallation(pushConfig);
            RegisterAsync(installation, CreateUPSHttpClient(pushConfig)).Wait();
        }

        public async Task RegisterAsync(Installation installation,  IUPSHttpClient client)
        {
            PushNotificationChannel channel = null;

            string error = null;
            try
            {
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                channel.PushNotificationReceived += OnPushNotification;
            }
            catch (Exception e) 
            {
                error = e.Message;
            }
            if (error != null)
            {
                await new MessageDialog("Error", error).ShowAsync();
            }

            ChannelStore channelStore = new ChannelStore();
            if (!channel.Uri.Equals(channelStore.Read()))
            {
                Debug.WriteLine("sending new token to UPS");
                installation.deviceToken = channel.Uri;
                using (client)
                {
                    channelStore.Save(channel.Uri);
                    HttpResponseMessage response = client.register(installation);
                }
            }
        }

        private IUPSHttpClient CreateUPSHttpClient(PushConfig pushConfig)
        {
            return new UPSHttpClient(pushConfig.UnifiedPushUri, pushConfig.VariantId, pushConfig.VariantSecret);
        }

        private void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            EventHandler<PushReceivedEvent> handler = PushReceivedEvent;
            if (handler != null)
            {
                handler(this, new PushReceivedEvent(e));
            }
        }

        private static Installation CreateInstallation(PushConfig pushConfig)
        {
            EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
            string os = deviceInformation.OperatingSystem;
            string deviceType = deviceInformation.SystemProductName;
            Installation installation = new Installation() { alias = pushConfig.Alias, operatingSystem = os, osVersion = deviceType, categories = pushConfig.Categories };
            return installation;
        }
    }
}
