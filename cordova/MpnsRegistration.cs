using System;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Phone.Notification;
using System.Collections.Generic;

namespace AeroGear.Push
{
    public class MpnsRegistration : Registration
    {
        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            string message = e.Collection["wp:Text1"];
            IDictionary<string, string> data = null;
            if (e.Collection.ContainsKey("wp:Param") && e.Collection["wp:Param"] != null)
            {
                data = UrlQueryParser.ParseQueryString(e.Collection["wp:Param"]);
            }
            OnPushNotification(message, data);
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
            }));
        }

        protected override Installation CreateInstallation(PushConfig pushConfig)
        {
            string operatingSystem = Environment.OSVersion.Platform.ToString();
            string osVersion = Environment.OSVersion.Version.ToString();
            Installation installation = new Installation() { alias = pushConfig.Alias, operatingSystem = operatingSystem, osVersion = osVersion, categories = pushConfig.Categories };
            return installation;
        }

        protected async override Task<string> ChannelUri()
        {
            HttpNotificationChannel channel;
            string channelName = "ToastChannel";

            channel = HttpNotificationChannel.Find(channelName);

            if (channel == null)
            {
                channel = new HttpNotificationChannel(channelName);
            }

            var tcs = new TaskCompletionSource<string>();
            channel.ChannelUriUpdated += (s, e) =>
            {
                tcs.TrySetResult(e.ChannelUri.ToString());
            };
            channel.ErrorOccurred += (s, e) =>
            {
                tcs.TrySetException(new Exception(e.Message));
            };

            channel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

            channel.Open();
            channel.BindToShellToast();
            return await tcs.Task;
        }

        protected override IChannelStore CreateChannelStore()
        {
            return new ChannelStore();
        }
    }
}
