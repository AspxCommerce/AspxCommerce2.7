using System;
using System.Configuration;

namespace NotificationSender
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
      XmlPost Req = new XmlPost(
        GetSetting("GoogleMerchantId"),
        GetSetting("GoogleMerchantKey"), 
        GetSetting("NotificationListenerUrl"),
        GetSetting("NotificationFile"), 
        int.Parse(GetSetting("TimeoutMilliseconds")));
      Console.WriteLine(Req.Send());
		}

    private static string GetSetting(string Key) {
      string RetVal = ConfigurationSettings.AppSettings[Key];
      if (RetVal == null || RetVal == string.Empty) {
        throw new ApplicationException(string.Format(
          "Add key '{0}' to config file", Key));
      }
      return RetVal;
    }

	}
}
