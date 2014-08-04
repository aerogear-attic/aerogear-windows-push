using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.UI.Popups;

namespace AeroGear.Push
{
    public class Registration
    {
        public event EventHandler<PushReceivedEvent> PushReceivedEvent;
        public event EventHandler<RegistrationCompleteEvent> RegistrationCompleteEvent;

        public void Register(PushConfig pushConfig)
        {
            RergisterAsync(pushConfig).Wait();
        }

        async Task RergisterAsync(PushConfig pushConfig)
        {

            Installation installation = CreateInstallation(pushConfig);
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
                installation.deviceToken = channel.Uri;
                using (var client = new UPSHttpClient(pushConfig.UnifiedPushUri))
                {
                    client.setCredentials(pushConfig.VariantId, pushConfig.VariantSecret);
                    client.register(installation);

                    channelStore.Save(channel.Uri);
                }
            }

            FireRegistrationCompleteEvent(channel.Uri);
        }

        private void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            EventHandler<PushReceivedEvent> handler = PushReceivedEvent;
            if (handler != null)
            {
                handler(this, new PushReceivedEvent(e));
            }
        }

        private void FireRegistrationCompleteEvent(string uri)
        {
            EventHandler<RegistrationCompleteEvent> handler = RegistrationCompleteEvent;
            if (handler != null)
            {
                handler(this, new RegistrationCompleteEvent(uri));
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
