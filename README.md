aerogear-windows-push
=====================

[![Build status](https://ci.appveyor.com/api/projects/status/dprb3dbsy5pwo7c7?svg=true)](https://ci.appveyor.com/project/edewit/aerogear-windows-push)

Push client sdk to register with UPS and enable push notifications on windows phone Windows Phone 8 (Silverlight-based) and Windows Phone App 8.1 (WinRT-based)

|                 | Project Info  |
| --------------- | ------------- |
| License:        | Apache License, Version 2.0  |
| Build:          | Visual Studio  |
| Documentation:  | https://aerogear.org/windows/  |
| Issue tracker:  | [https://issues.jboss.org/browse/AGWIN](https://issues.jboss.org/browse/AGWIN)  |
| Mailing lists:  | [aerogear-users](http://aerogear-users.1116366.n5.nabble.com/) ([subscribe](https://lists.jboss.org/mailman/listinfo/aerogear-users))  |
|                 | [aerogear-dev](http://aerogear-dev.1069024.n5.nabble.com/) ([subscribe](https://lists.jboss.org/mailman/listinfo/aerogear-dev))  |

## Usage

Add the NuGet package to your project and add the following code:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    PushConfig pushConfig = new PushConfig() {
        UnifiedPushUri = new Uri(""), VariantId = "", VariantSecret = ""
    }; //[1]
    Registration registration = new WnsRegistration();      // [2]
    registration.PushReceivedEvent += HandleNotification;
    registration.Register(pushConfig);
}

void HandleNotification(object sender, PushReceivedEvent e)
{
    Debug.WriteLine(e.Args.message);
}
```

* [1] add the url, variantId and varaintSecret of you Unified Push server
* [2] choose WnsRegistration for wns otherwise MpnsRegistration.

## Documentation

For more details about the current release, please consult [our documentation](https://aerogear.org/windows/).

## Development

If you would like to help develop AeroGear you can join our [developer's mailing list](https://lists.jboss.org/mailman/listinfo/aerogear-dev), join #aerogear on Freenode, or shout at us on Twitter @aerogears.

Also takes some time and skim the [contributor guide](http://aerogear.org/docs/guides/Contributing/)

## Questions?

Join our [user mailing list](https://lists.jboss.org/mailman/listinfo/aerogear-users) for any questions or help! We really hope you enjoy app development with AeroGear!

## Found a bug?

If you found a bug please create a ticket for us on [Jira](https://issues.jboss.org/browse/AGWIN) with some steps to reproduce it.
