using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Popups;

namespace AeroGear.Push
{
    public class WnsRegistration : Registration
    {
        protected async override Task Register(Installation installation, IUPSHttpClient client)
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
                //Debug.WriteLine("sending new token to UPS");
                installation.deviceToken = channel.Uri;
                channelStore.Save(channel.Uri);
                HttpStatusCode response = await client.register(installation);
            }
        }

        private void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            string message = args.ToastNotification.Content.InnerText;
            OnPushNotification(message, null);
        }

        protected override Installation CreateInstallation(PushConfig pushConfig)
        {
            EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
            string os = deviceInformation.OperatingSystem;
            string deviceType = deviceInformation.SystemProductName;
            Installation installation = new Installation() { alias = pushConfig.Alias, operatingSystem = os, osVersion = deviceType, categories = pushConfig.Categories };
            return installation;

        }
    }
}
