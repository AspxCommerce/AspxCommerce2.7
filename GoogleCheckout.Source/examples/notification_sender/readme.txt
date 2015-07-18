This app can test your notification listener by sending notification XML to it. 
This may be easier in your early testing for two reasons:

1. You can test notification processing under more controlled circumstances.

2. You can test on a machine that is not visible to the public Internet, like 
your local work station.


It's a console app which takes no parameters. The app pulls all the data it 
needs from the config file. These are the keys:

GoogleMerchantId - Your Merchant Id. Will be used to create the Basic 
Authentication header.

GoogleMerchantKey - Your Merchant Key. Will be used to create the Basic 
Authentication header.

NotificationListenerUrl - The URL of your notification listener, ie. your page 
that receives and processes notifications.

NotificationFile - Relative or absolute path to a text file that contains the 
notification XML that should be sent to the notification listener.

TimeoutMilliseconds - Number of milliseconds to wait for a response before 
giving up and reporting a timeout.
