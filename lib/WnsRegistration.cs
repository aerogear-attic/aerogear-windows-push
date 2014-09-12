﻿using System;
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
    }
}