/**
 * JBoss, Home of Professional Open Source
 * Copyright Red Hat, Inc., and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * 	http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
    /// <summary>
    /// Wns based version
    /// </summary>
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

        protected override ILocalStore CreateChannelStore()
        {
            return new LocalStore();
        }
    }
}
