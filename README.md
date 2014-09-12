aerogear-windows-push
=====================

Push client sdk to register with UPS and enable push notifications on windows phone Windows Phone 8 (Silverlight-based) and
Windows Phone App 8.1 (WinRT-based)

Add the NuGet package to your project and add the following code:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    PushConfig pushConfig = new PushConfig() { UnifiedPushUri = new Uri(""), VariantId = "", VariantSecret = "" }; //[1]
    Registration registration = new WnsRegistration();      // [2]
    registration.PushReceivedEvent += HandleNotification;
    registration.Register(pushConfig);
}

void HandleNotification(object sender, PushReceivedEvent e)
{
    Debug.WriteLine(e.Args.message);
}
```

[1] add the url, variantId and varaintSecret of you Unified Push server
[2] choose WnsRegistration for wns otherwise MpnsRegistration.
