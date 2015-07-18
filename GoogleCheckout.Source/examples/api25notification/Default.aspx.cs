using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

//original source from http://www.capprime.com/software_development_weblog/2010/11/29/UsingTheGoogleCheckout25APIWithASPNETMVCTheMissingSample.aspx

public partial class _Default : System.Web.UI.Page {

  protected void Page_Load(object sender, EventArgs e) {
    string serialNumber = null;

    // Receive Request
    Stream requestInputStream = Request.InputStream;
    
    string requestStreamAsString = null;
    using (System.IO.StreamReader oneStreamReader = new StreamReader(requestInputStream)) {
      requestStreamAsString = oneStreamReader.ReadToEnd();
    }

    // Parse Request to retreive serial number
    string[] requestStreamAsParts = requestStreamAsString.Split(new char[] { '=' });
    if (requestStreamAsParts.Length >= 2) {
      serialNumber = requestStreamAsParts[1];
    }

    // Call NotificationHistory Google Checkout API to retrieve the notification for the given serial number and process the notification
    GoogleCheckoutHelper.ProcessNotification(serialNumber);

    //serialize the message to the output stream only if you could process the message.
    //Otherwise throw an http 500.

    var response = new GCheckout.AutoGen.NotificationAcknowledgment();
    response.serialnumber = serialNumber;

    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.BinaryWrite(GCheckout.Util.EncodeHelper.Serialize(response));
  }

}
