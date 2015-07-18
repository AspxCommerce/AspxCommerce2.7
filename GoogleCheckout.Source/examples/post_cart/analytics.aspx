<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="GCheckout.Checkout" %>
<%@ Import Namespace="GCheckout.Util" %>
<%@ Register TagPrefix="cc1" Namespace="GCheckout.Checkout" Assembly="GCheckout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server" language="c#">
  
  /*
   This sample shows how to implement 
   * http://code.google.com/apis/checkout/developer/checkout_analytics_integration.html
   * Part II: Integrating Google Analytics into your Checkout API request
   */
  private void PostCartToGoogle(object sender, 
    System.Web.UI.ImageClickEventArgs e) {
    CheckoutShoppingCartRequest Req = GCheckoutButton1.CreateRequest();
    //TODO: This next line is the only line required 
    //to add the analytics data to the request.
    //This line will add the code needed for 
    //Part II: Integrating Google Analytics into your Checkout API request
    Req.AnalyticsData = HttpContext.Current.Request["analyticsdata"]; //required

    Req.AddItem("Mars bar", "Packed with peanuts", 0.75m, 2);

    //lets make sure we can add 2 different flat rate shipping amounts
    Req.AddFlatRateShippingMethod("UPS Ground", 5);

    //Add a rule to tax all items at 7.5% for Ohio
    Req.AddStateTaxRule("OH", 7.5, true);
    
    GCheckoutResponse Resp = Req.Send();
    if (Resp.IsGood) {
      Response.Redirect(Resp.RedirectUrl, true);
    }
    else {
      Response.Write("Resp.ResponseXml = " + Resp.ResponseXml + "<br>");
      Response.Write("Resp.RedirectUrl = " + Resp.RedirectUrl + "<br>");
      Response.Write("Resp.IsGood = " + Resp.IsGood + "<br>");
      Response.Write("Resp.ErrorMessage = " + Resp.ErrorMessage + "<br>");
    }

  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
</head>
<body>
  <!--TODO Please make sure you include the onsubmit code as seen below.
  without this, the analyticsdata input will NOT be set-->
  <form name="googleForm" id="googleForm" runat="server" onsubmit="setUrchinInputCode();">
    <input type="hidden" name="checkout" id="checkout" value="1" />
    <!--You must make sure this hidden form field is included. DO NOT change the case or set run at server-->
    <input type="hidden" name="analyticsdata" id="analyticsdata" value="" />
    <div>
      <cc1:GCheckoutButton runat="server" OnClick="PostCartToGoogle" ID="GCheckoutButton1"
        name="GCheckoutButton1" />
    </div>
  </form>

  <script src="http://checkout.google.com/files/digital/urchin_post.js" type="text/javascript"></script>

  <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>

  <script type="text/javascript">
	  _uacct = "UA-0000000-0"; //TODO Place your Google Analytics Account number here. The format is usually UA-0000000-1
	  urchinTracker();
  </script>

</body>
</html>
