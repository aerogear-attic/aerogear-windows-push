using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Popups;

namespace AeroGear.Push
{
    public class WnsRegistration : Registration
    {
        private void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            string message = args.ToastNotification.Content.InnerText;

            var launch = args.ToastNotification.Content.GetElementsByTagName("toast")[0].Attributes.GetNamedItem("launch");
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (launch != null)
            {
                data = UrlQueryParser.ParseQueryString(launch.InnerText);
            }

            OnPushNotification(message, data);
        }

        protected override Installation CreateInstallation(PushConfig pushConfig)
        {
            EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
            string os = deviceInformation.OperatingSystem;
            string deviceType = deviceInformation.SystemProductName;
            Installation installation = new Installation() { alias = pushConfig.Alias, operatingSystem = os, osVersion = deviceType, categories = pushConfig.Categories };
            return installation;
        }

        protected async override Task<string> ChannelUri()
        {
            PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            channel.PushNotificationReceived += OnPushNotification;
            return channel.Uri;
        }

        protected override IChannelStore CreateChannelStore()
        {
            return new ChannelStore();
        }
    }
}
