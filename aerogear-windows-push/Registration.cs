using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace AeroGear.Push
{
    public class Registration
    {
        public void Register(PushConfig pushConfig, MobileServiceClient mobileServiceClient)
        {
            RergisterAsync(pushConfig, mobileServiceClient).Wait();
        }

        static async Task RergisterAsync(PushConfig pushConfig, MobileServiceClient mobileServiceClient)
        {

            EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
            string os = deviceInformation.OperatingSystem;
            string deviceType = deviceInformation.SystemProductName;

            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            Installation installation = new Installation() { alias = pushConfig.Alias, operatingSystem = os, osVersion = deviceType, categories = pushConfig.Categories, deviceToken = channel.Uri };

            using (var client = new HttpClient())
            {
                client.BaseAddress = pushConfig.UnifiedPushUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(pushConfig.VariantId + ":" + pushConfig.VariantSecret)));

                HttpResponseMessage response = await client.PostAsJsonAsync<Installation>("rest/registry/device", installation);

                response.EnsureSuccessStatusCode();

                await mobileServiceClient.GetPush().RegisterNativeAsync(channel.Uri);
            }
        }
    }
}
